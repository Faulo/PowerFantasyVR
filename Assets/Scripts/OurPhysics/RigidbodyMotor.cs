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

        [SerializeField, Range(0, 10)]
        public float upwardsDrag = 0;
        [SerializeField, Range(0, 10)]
        public float downwardsDrag = 0;
        [SerializeField, Range(0, 10)]
        public float horizontalDrag = 0;

        void Start() {
            rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate() {
            if (upwardsDrag > 0 && velocity.y > 0) {
                LerpVelocity(new Vector3(velocity.x, 0, velocity.z), upwardsDrag * Time.deltaTime);
            }
            if (downwardsDrag > 0 && velocity.y < 0) {
                LerpVelocity(new Vector3(velocity.x, 0, velocity.z), downwardsDrag * Time.deltaTime);
            }
            if (horizontalDrag > 0) {
                LerpVelocity(new Vector3(0, velocity.y, 0), horizontalDrag * Time.deltaTime);
            }
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