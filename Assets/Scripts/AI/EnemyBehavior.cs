using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

        [SerializeField, Range(0, 1000)]
        private float idleDiffusion = 100.0f;
        private float chasingDiffusion = 0.0f;

        private float diffusion;
        private float goalDiffusion;
        private float randNum;
        private float diffusionX;
        private float diffusionY;
        private float diffusionZ;

        private Rigidbody thisRigidbody;
        private LeaderBehavior leaderBehavior;
        private Transform nearestGoal;
        private Transform currentBeacon;

        private Vector3 diffusionVector;
        private Vector3 finalMovementVector;
        private Vector3 finalGoalMovementVector;
        private Vector3 transformationVector;


        private GameObject[] arrayOfBeacons;
        private GameObject leaders;

        // Start is called before the first frame update
        void Start()
        {
            // Retrieve all Beacons
            arrayOfBeacons = GameObject.FindGameObjectsWithTag("Beacon");
            leaders = GameObject.FindGameObjectWithTag("Leaders");
            thisRigidbody = GetComponent<Rigidbody>();

            StartCoroutine(FindGoalRoutine());
            leaderBehavior = (LeaderBehavior) leaders.GetComponent<LeaderBehavior>();
            nearestGoal = arrayOfBeacons[0].transform;
        }

        IEnumerator FindGoalRoutine()
        {
            var wait = new WaitForSeconds(0.1f);
            while (true)
            {
                // *** Part 1: Find nearest goal ***
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

        // Update is called once per frame
        // Guidance System for agents is implemented here
        void Update()
        {
            if (!nearestGoal) {
                return;
            }
            // *** Part 2: Calculate drift ***
            // Set nearest goal
            transformationVector = new Vector3(nearestGoal.position.x, nearestGoal.position.y, nearestGoal.position.z) - transform.position;
            // *** Part 3: Calculate diffusion ***
            // Create Diffusion Term: Distance between beacon/goal and agent multiplied with beta
            diffusion = Vector3.Distance(nearestGoal.position, transform.position) * betaFactor;

            // *** Part 4: Create Random Factor and add to x and z diffusion ***
            // Adjust x-factor
            randNum = MarsagliaGenerator.Next();
            diffusionX = diffusion * randNum;
            // Adjust y-factor
            randNum = MarsagliaGenerator.Next();
            diffusionY = diffusion * randNum;
            // Adjust z-factor
            randNum = MarsagliaGenerator.Next(); 
            diffusionZ = diffusion * randNum;

            // *** Part 5: Put together the parts ***
            // Diffusion vector: position of goal + position of diffusion vector minus the position of the agent
            diffusionVector = new Vector3(nearestGoal.position.x + diffusionX, nearestGoal.position.y + diffusionY, nearestGoal.position.z + diffusionZ) - transform.position;
            
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
                //alphaFactorUsed = alphaFactor;
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
                //alphaFactorUsed = alphaFactor + 1.0f;
            }


            finalMovementVector = transformationVector * alphaFactorUsed + diffusionVector;

            thisRigidbody.AddRelativeForce(finalMovementVector);
        }
    }
}
