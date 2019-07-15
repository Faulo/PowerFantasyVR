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
        private float alphaFactor = 0.4f;
        // Beta > 0; Factor for diffusion intensity
        private float betaFactor = 1.0f;
        private float diffusion;
        private float goalDiffusion;
        private float randNum;
        private float diffusionX;
        private float diffusionY;
        private float diffusionZ;

        private float nearestGoalDistance = 10000.0f;
        private float currentDistance = 10000.0f;
        private float goalRadius = 0.0f;
        private Transform nearestGoal;

        private Vector3 finalMovementVector;
        private Vector3 finalGoalMovementVector;
        
        private GameObject[] arrayOfBeacons;
        private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            // Retrieve all Beacons
            arrayOfBeacons = GameObject.FindGameObjectsWithTag("Beacon");

            //Retrieve player
            player = GameObject.FindGameObjectWithTag("Player");
            //if(goals != null)
            //{
            //    foreach (Transform each in goals.transform)
            //    {
            //        listOfGoals.Add(each);
            //    }
            //}
        }

        // Update is called once per frame
        // Guidance System for agents is implemented here
        void Update()
        {
            // *** Part 1: Find nearest goal ***
            // Reset value for the nearest goalfor new calculation
            nearestGoalDistance = float.MaxValue;

            // Find player nearby
            //if(player != null) { 
            //    currentDistance = Vector3.Distance(goal.position, transform.position);
            //    if (currentDistance < nearestGoalDistance)
            //    {
            //        nearestGoalDistance = currentDistance;
            //        nearestGoal = goal;
            //        // take half of scale to get the radius
            //        goalRadius = goal.Find("Radius").transform.lossyScale.x / 2.0f;
            //    }
            //}

            // Find nearest beacon and set as goal if nearer than current goal and not in goal radius
            Transform currentBeacon;
            for (int i=0; i < arrayOfBeacons.Length; i++)
            {
                currentBeacon = arrayOfBeacons[i].transform;
                currentDistance = Vector3.Distance(currentBeacon.position, transform.position);
                if (currentDistance < nearestGoalDistance && nearestGoalDistance > goalRadius)
                {
                    nearestGoalDistance = currentDistance;
                    nearestGoal = currentBeacon;
                }
            }

            // *** Part 2: Calculate drift ***
            // Set nearest goal
            Vector3 transformationVector = new Vector3(nearestGoal.position.x, nearestGoal.position.y, nearestGoal.position.z) - transform.position;

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

            // Diffusion vector: position of goal + position of diffusion vector minus the position of the agent
            Vector3 diffusionVector = new Vector3(nearestGoal.position.x + diffusionX, nearestGoal.position.y + diffusionY, nearestGoal.position.z + diffusionZ) - transform.position;

            // *** Part 5: Put together the parts ***
            finalMovementVector = transformationVector * alphaFactor + diffusionVector;

            GetComponent<Rigidbody>().AddRelativeForce(finalMovementVector);
        }
    }
}
