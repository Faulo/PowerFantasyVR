using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMotor : MonoBehaviour, IMotor {
        private new Rigidbody rigidbody;

        public Vector3 position => transform.position;
        public Vector3 velocity {
            get => rigidbody.velocity;
            private set => rigidbody.velocity = value;
        }
        public float speed => velocity.magnitude;

        void Start() {
            rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate() {

        }

        public void AddVelocity(Vector3 addedVelocity) {
            rigidbody.AddForce(addedVelocity, ForceMode.VelocityChange);
        }

        public void Break(float breakFactor) {
            LerpVelocity(Vector3.zero, breakFactor);
        }

        public void LerpVelocity(Vector3 targetVelocity, float factor) {
            velocity = Vector3.Lerp(velocity, targetVelocity, factor);
        }
    }
}