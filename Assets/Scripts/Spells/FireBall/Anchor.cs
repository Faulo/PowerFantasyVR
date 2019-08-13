using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.Spells.FireBall {
    public class Anchor : MonoBehaviour {
        [SerializeField, Range(0, 100)]
        private float maximumSpeed = 10;
        [SerializeField, Range(0, 100)]
        private float accelerationSpeed = 10;

        private Transform target;
        private Vector3 targetVelocity;

        public void ConnectTo(Ball ball) {
            target = ball.transform;
            targetVelocity = Vector3.zero;

            ball.explodable = false;
            ball.transform.position = transform.position;
            ball.rigidbody.isKinematic = true;
            ball.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }

        public void ReleaseFrom(Ball ball) {
            ball.explodable = true;
            ball.transform.parent = ball.transform.parent.parent;
            ball.rigidbody.isKinematic = false;
            ball.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            ball.rigidbody.velocity = targetVelocity;
            target = null;
        }

        private void Update() {
            if (target) {
                targetVelocity = Vector3.Lerp(targetVelocity, maximumSpeed * (transform.position - target.position), accelerationSpeed * Time.deltaTime);
                target.Translate(targetVelocity * Time.deltaTime);
            }
        }
    }
}