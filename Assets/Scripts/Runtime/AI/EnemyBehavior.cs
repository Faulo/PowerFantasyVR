﻿using System;
using System.Collections;
using PFVR.OurPhysics;
using PFVR.Player;
using UnityEngine;

namespace PFVR.AI {
    /**
     * <summary>
     * Movement skript for one enemy object. Movement depending on current location, player contact and distance from home area. This is the base class for moving swarm objects. The abstract method <c>MoveObject</c> must be implemented.</summary>
     **/
    public abstract class EnemyBehavior : MonoBehaviour {
        /** <summary><value>The <c>alphaFactor</c> defines the factor for calculating the drift (alpha > 0).</value></summary>*/
        [SerializeField, Range(0, 5)]
        float alphaFactor = 0.4f;

        /** <summary><value>The <c>betaFactor</c> defines the factor for calculating the diffusion intensity (beta > 0).</value></summary>*/
        [SerializeField, Range(0, 10)]
        float betaFactor = 1.0f;

        /** <summary><value>The <c>playerVelocityThreshold</c> defines the minimum velocity of the player for starting to evade attack.</value></summary>*/
        [SerializeField, Range(0, 200.0f)]
        float playerVelocityTheshold = 100.0f;

        /** <summary><value>The <c>playerDistanceThreshold</c> defines the maximum distance of the player before starting to chase them.</value></summary>*/
        [SerializeField, Range(0, 1000)]
        float playerDistanceThreshold = 30.0f;

        /** <summary><value>The <c>idleDiffusion</c> defines the diffusion value when idle.</value></summary>*/
        [SerializeField, Range(0, 1000)]
        float idleDiffusion = 100.0f;

        /** <summary><value>The <c>idleDiffusion</c> defines the diffusion value when chasing.</value></summary>*/
        [SerializeField, Range(0, 1000)]
        float chasingDiffusion = 0.0f;

        // Final movement vector (protected)
        protected Vector3 finalMovementVector;

        // Related scripts
        LeaderBehavior leaderBehavior;

        // Transformation calculations
        float alphaFactorUsed;
        float randNum;
        Transform nearestGoal;
        Vector3 diffusionVector;
        Vector3 transformationVector;
        Vector3 randomVector;
        Vector3 nearestGoalPosition;
        Vector3 transformPosition;

        // Game Objects for pathing
        GameObject[] arrayOfBeacons;

        // Evation variables
        IMotor playerMotor => PlayerBehaviour.instance.motor;

        bool evadePlayer;
        float playerVelocity;
        float playerEnemyDistance;
        Vector3 evadeVector;
        Func<Vector3, Vector3, Vector3> evadeBehavior;

        // Animation
        Animator animator;

        // Start is called before the first frame update
        protected virtual void Start() {
            // Retrieve all Beacons
            arrayOfBeacons = GameObject.FindGameObjectsWithTag("Beacon");
            leaderBehavior = GameObject.FindGameObjectWithTag("Leaders").GetComponent<LeaderBehavior>();

            evadeVector = new Vector3();
            evadeBehavior = EnemyEvasion.FindEvadeBehavior(UnityEngine.Random.Range(0, 4));

            animator = GetComponent<Animator>();
            StartCoroutine(FindGoalRoutine());
            StartCoroutine(AnimateAlert());
            StartCoroutine(EvadePlayerRoutine());
        }

        IEnumerator AnimateAlert() {
            var wait = new WaitForSeconds(1.0f);
            while (true) {
                if (leaderBehavior.chasePlayer) {
                    animator.SetBool("Alerted", true);
                } else {
                    animator.SetBool("Alerted", false);
                }

                yield return wait;
            }
        }

        IEnumerator FindGoalRoutine() {
            var wait = new WaitForSeconds(1.0f);
            while (true) {
                // *** Find nearest goal ***
                // Reset value for the nearest goal for new calculation
                float nearestGoalDistance = float.MaxValue;

                // Find nearest beacon and set as goal if nearer than current goal
                for (int i = 0; i < arrayOfBeacons.Length; i++) {
                    var currentBeacon = arrayOfBeacons[i].transform;
                    float currentDistance = Vector3.Distance(currentBeacon.position, transform.position);
                    if (currentDistance < nearestGoalDistance) {
                        nearestGoalDistance = currentDistance;
                        nearestGoal = currentBeacon;
                        nearestGoalPosition = nearestGoal.position;
                    }
                }

                yield return wait;
            }
        }

        IEnumerator EvadePlayerRoutine() {
            var wait = new WaitForSeconds(0.1f);
            while (true) {
                playerVelocity = playerMotor.velocity.magnitude;
                playerEnemyDistance = Vector3.Distance(transform.position, playerMotor.position);
                // Evade the player when they have at least a minimum speed AND only when near
                if (playerVelocity > playerVelocityTheshold && playerEnemyDistance < playerDistanceThreshold) {
                    evadePlayer = true;
                }
                // If player is slower than threshold OR player has passed AND enemies are already evading, then stop evading player and set the evation vector anew
                else if ((playerVelocity < playerVelocityTheshold || playerEnemyDistance > playerDistanceThreshold) && evadePlayer) {
                    evadePlayer = false;
                    evadeVector = new Vector3();
                }

                yield return wait;
            }
        }

        // Defines the diffusion vector
        void CreateAdditionalDiffusion(float diffusionValue) {
            randomVector = MarsagliaGenerator.NextVector3();
            diffusionVector += diffusionValue * randomVector;
        }

        // Update is called once per frame
        // Guidance System for agents is implemented here
        protected void FixedUpdate() {
            transformPosition = transform.position;
            if (!nearestGoal) {
                return;
            }
            // *** Calculate drift ***
            // Set nearest goal
            transformationVector = new Vector3(nearestGoalPosition.x, nearestGoalPosition.y, nearestGoalPosition.z) - transformPosition;
            alphaFactorUsed = alphaFactor;

            // *** Evade Player ***
            if (evadePlayer) {
                //Debug.Log("Evade the Player detected!");
                //Find out the direction from which player is coming and evade to the sides! Set Vector only in first frame of evation.
                if (evadeVector.magnitude <= 0) {
                    evadeVector = evadeBehavior(playerMotor.position, transformPosition);
                }

                transformationVector = evadeVector * 7.0f;
                alphaFactorUsed = alphaFactor + 30.0f;
            }

            // *** Create diffusion ***
            randomVector = MarsagliaGenerator.NextVector3();
            // Diffusion vector: position of goal + position of diffusion vector minus the position of the agent
            diffusionVector = transformationVector + (betaFactor * Vector3.Distance(nearestGoal.position, transformPosition) * randomVector);

            // Additional chasing behavior
            // Diffuse more when not chasing player
            if (!leaderBehavior.chasePlayer) {
                // Add more diffusion when idle
                CreateAdditionalDiffusion(idleDiffusion);
            } else {
                // Concentrate / Diffuse less when chasing player
                CreateAdditionalDiffusion(chasingDiffusion);
            }

            // *** Put together the parts ***
            finalMovementVector = (transformationVector * alphaFactorUsed) + diffusionVector;

            // *** Move the object ***
            MoveObject();
        }

        // How to move the object according to the calculated vector
        public abstract void MoveObject();

    }
}
