using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using PFVR.OurPhysics;
using PFVR.Player;

namespace PFVR.AI
{
    // Additional behavior for enemies (besides homing in on a target)
    public class EnemyBehavior : MonoBehaviour
    {
        // Alpha > 0; Factor for velocity of agents
        [SerializeField, Range(0, 10)]
        private float alphaFactor = 0.4f;
        private float alphaFactorUsed;
        // Beta > 0; Factor for diffusion intensity

        [SerializeField, Range(0, 10)]
        private float betaFactor = 1.0f;

        [SerializeField, Range(0, 200.0f)]
        private float playerVelocityTheshold = 100.0f;

        [SerializeField, Range(0, 1000)]
        private float idleDiffusion = 100.0f;
        private float chasingDiffusion = 0.0f;

        private float diffusion;
        private float goalDiffusion;
        private float randNum;
        private float diffusionX;
        private float diffusionY;
        private float diffusionZ;
        
        private LeaderBehavior leaderBehavior;
        private Transform nearestGoal;

        private Vector3 diffusionVector;
        private Vector3 finalMovementVector;
        private Vector3 finalGoalMovementVector;
        private Vector3 transformationVector;
        private Vector3 randomVector;
        private Vector3 evadeVector;
        private float playerVelocity;


        private GameObject[] arrayOfBeacons;
        private GameObject leaders;
        private PlayerBehaviour playerBehavior;
        private IMotor playerMotor;
        private bool evadePlayer;

        private int evadeDirection;
        private Func<Vector3, Vector3, Vector3> evadeBehavior;


        // Start is called before the first frame update
        void Start()
        {
            // Retrieve all Beacons
            arrayOfBeacons = GameObject.FindGameObjectsWithTag("Beacon");
            leaders = GameObject.FindGameObjectWithTag("Leaders");

            leaderBehavior = leaders.GetComponent<LeaderBehavior>();
            nearestGoal = arrayOfBeacons[0].transform;

            playerBehavior = leaderBehavior.GetPlayer();
            //playerRigidbody = playerBehavior.GetComponent<Rigidbody>();
            playerMotor = playerBehavior.motor;

            evadeVector = new Vector3();
            evadeDirection = UnityEngine.Random.Range(0, 3);
            evadeBehavior = EnemyEvation.FindEvadeBehavior(evadeDirection);

            StartCoroutine(FindGoalRoutine());
            //StartCoroutine(EvadePlayerRoutine());
        }

        IEnumerator FindGoalRoutine()
        {
            var wait = new WaitForSeconds(1.0f);
            while (true)
            {
                // *** Find nearest goal ***
                // Reset value for the nearest goal for new calculation
                var nearestGoalDistance = float.MaxValue;

                // Find nearest beacon and set as goal if nearer than current goal
                for (int i = 0; i < arrayOfBeacons.Length; i++)
                {
                    var currentBeacon = arrayOfBeacons[i].transform;
                    var currentDistance = Vector3.Distance(currentBeacon.position, transform.position);
                    if (currentDistance < nearestGoalDistance)
                    {
                        nearestGoalDistance = currentDistance;
                        nearestGoal = currentBeacon;
                    }
                }
                yield return wait;
            }
        }

        IEnumerator EvadePlayerRoutine()
        {
            var wait = new WaitForSeconds(0.1f);
            while (true)
            {
                playerVelocity = playerMotor.velocity.magnitude;
                if (playerVelocity > playerVelocityTheshold && Vector3.Distance(transform.position, playerMotor.position) < 30.0f)
                {
                    evadePlayer = true;
                    Debug.Log("Set Evade Player true");
                }
                else if (playerVelocity < playerVelocityTheshold && evadePlayer)
                {
                    evadePlayer = false;
                    if (evadeVector.magnitude > 0)
                    {
                        evadeVector = new Vector3();
                    }
                    //Debug.Log("Set Evade Player false");
                }
                yield return wait;
            }
        }
        
        // Update is called once per frame
        // Guidance System for agents is implemented here
        void Update()
        {
            if (!nearestGoal) {
                return;
            }
            // *** Calculate drift ***
            // Set nearest goal
            transformationVector = new Vector3(nearestGoal.position.x, nearestGoal.position.y, nearestGoal.position.z) - transform.position;
            alphaFactorUsed = alphaFactor;

            // *** Create Random Factor for diffusion ***
            randomVector = MarsagliaGenerator.NextVector3();

            // *** Evade Player ***
            // Todo: Get it working! Aktuell kummuliert hier irgendwas, sodass es immer mehr lagged, je öfter der player durchfliegt!
            if(evadePlayer)
            {
                Debug.Log("Evade the Player detected!");
                //Find out the direction from which player is coming and evade to the sides!
                evadeBehavior = EnemyEvation.FindEvadeBehavior(evadeDirection);
                if (evadeVector.magnitude <= 0.0f)
                {
                    evadeVector = evadeBehavior(playerMotor.position, transform.position);
                }
                transformationVector = evadeVector;
                alphaFactorUsed = alphaFactor + 30.0f;
            }


            // *** Put together the parts ***
            // Diffusion vector: position of goal + position of diffusion vector minus the position of the agent
            diffusionVector = transformationVector + randomVector * Vector3.Distance(nearestGoal.position, transform.position) * betaFactor;
            //diffusionVector = new Vector3(nearestGoal.position.x + diffusionX, nearestGoal.position.y + diffusionY, nearestGoal.position.z + diffusionZ) - transform.position;
            
            // Additional chasing behavior
            // Diffuse more when not chasing player
            if (!leaderBehavior.chasePlayer)
            {
                // Add more diffusion when idle
                randNum = MarsagliaGenerator.Next();
                diffusionVector.x += idleDiffusion * randNum;
                randNum = MarsagliaGenerator.Next();
                diffusionVector.y += idleDiffusion * randNum;
                randNum = MarsagliaGenerator.Next();
                diffusionVector.z += idleDiffusion * randNum;
            }
            else
            {
                // Concentrate / Diffuse less when chasing player
                randNum = MarsagliaGenerator.Next();
                diffusionVector.x += chasingDiffusion * randNum;
                randNum = MarsagliaGenerator.Next();
                diffusionVector.y += chasingDiffusion * randNum;
                randNum = MarsagliaGenerator.Next();
                diffusionVector.z += chasingDiffusion * randNum;
            }
            
            finalMovementVector = transformationVector * alphaFactorUsed + diffusionVector;

            //thisRigidbody.AddRelativeForce(finalMovementVector);
            transform.Translate(finalMovementVector * Time.deltaTime);
        }
    }
}
