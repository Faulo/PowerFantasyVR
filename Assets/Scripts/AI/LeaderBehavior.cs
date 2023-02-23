using PFVR.OurPhysics;
using PFVR.Player;
using UnityEngine;

namespace PFVR.AI {
    /**
     * <summary>Movement script for the group of leaders. Homes in on each target after another.</summary>
     **/
    public class LeaderBehavior : MonoBehaviour {
        /** <summary><value>The <c>alphaFactor</c> defines the factor for calculating the drift (alpha > 0).</value></summary>*/
        public float alphaFactor = 0.2f;
        float alphaFactorUsed;
        /** <summary><value>The <c>attackPlayerDistance</c> defines the distance before attacking the player.</value></summary>*/
        public float attackPlayerDistance = 500.0f;
        /** <summary><value>The <c>ankorThreshold</c> defines the distance to the home area before returning.</value></summary>*/
        public float ankorThreshold = 1000.0f;
        /** <summary><value>The <c>chasePlayer</c> sets a boolean value for deciding if player is currently chased.</value></summary>*/
        public bool chasePlayer = false;

        IMotor motor => PlayerBehaviour.instance.motor;
        GameObject ankor;
        GameObject[] arrayOfTargets;

        Vector3 currentTarget;
        float currentDistance;
        int num;
        float nearTarget = 10.0f;
        float playerDistance;
        float ankorDistance;
        bool lookForPlayer = false;
        bool normalize = false;
        Vector3 transformationVector;

        // Start is called before the first frame update
        void Start() {
            // Retrieve all targets
            arrayOfTargets = GameObject.FindGameObjectsWithTag("LeaderTarget");
            //Retrieve ankor
            ankor = GameObject.FindGameObjectWithTag("Ankor");
            // Set number of Targets
            num = arrayOfTargets.Length - 1;
            lookForPlayer = true;
        }

        // Update is called once per frame
        void Update() {
            alphaFactorUsed = alphaFactor;
            // Find next target
            if (num < 0) {
                num = arrayOfTargets.Length - 1;
            }
            currentTarget = arrayOfTargets[num].transform.position;

            currentDistance = Vector3.Distance(currentTarget, transform.position);
            if (currentDistance < nearTarget) {
                num--;
                normalize = false;
                lookForPlayer = true;
            }
            // Find player nearby
            if (motor != null) {
                playerDistance = Vector3.Distance(motor.position, transform.position);
                ankorDistance = Vector3.Distance(ankor.transform.position, transform.position);
                if (playerDistance < attackPlayerDistance && ankorDistance < ankorThreshold && lookForPlayer) {
                    currentTarget = motor.position;
                    chasePlayer = true;
                    alphaFactorUsed = alphaFactor + 2.0f;

                } else if (ankorDistance > ankorThreshold) {
                    lookForPlayer = false;
                    chasePlayer = false;
                    alphaFactorUsed = alphaFactor;
                    normalize = true;
                }
            }

            // Move leader to target
            transformationVector = new Vector3(currentTarget.x, currentTarget.y, currentTarget.z) - transform.position;
            if (normalize) {
                Vector3.Normalize(transformationVector);
                transformationVector = transformationVector * 0.4f;
            }
            //rigidBody.AddRelativeForce(transformationVector * alphaFactorUsed);
            transform.Translate(transformationVector * alphaFactorUsed * Time.deltaTime);
        }
    }
}