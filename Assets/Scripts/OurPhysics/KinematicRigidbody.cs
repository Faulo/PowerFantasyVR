using UnityEngine;

namespace PFVR.OurPhysics {
    public class KinematicRigidbody : MonoBehaviour {
        [HideInInspector]
        public float mass = 1;
        [HideInInspector]
        public Vector3 velocity = Vector3.zero;
        [SerializeField]
        public bool transferForce = true;

        private Rigidbody body => GetComponent<Rigidbody>();

        void Start() {
            if (body != null) {
                mass = body.mass;
                velocity = body.velocity;
                body.isKinematic = true;
            }
        }

        void FixedUpdate() {
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        private void OnCollisionEnter(Collision collision) {
            if (transferForce && collision.rigidbody != null) {
                collision.rigidbody.AddForce(velocity * mass, ForceMode.Impulse);
            }
        }
    }
}