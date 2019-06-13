using UnityEngine;

namespace PFVR.OurPhysics {
    public class KinematicRigidbody : MonoBehaviour {
        [HideInInspector]
        public float mass = 1;
        [HideInInspector]
        public Vector3 velocity = Vector3.zero;

        private Rigidbody body {
            get => GetComponent<Rigidbody>();
        }

        void Start() {
            if (body != null) {
                mass = body.mass;
                velocity = body.velocity;
                body.isKinematic = true;
            }
        }

        void FixedUpdate() {
            transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.rigidbody != null) {
                collision.rigidbody.AddForce(velocity * mass, ForceMode.Impulse);
            }
        }
    }
}