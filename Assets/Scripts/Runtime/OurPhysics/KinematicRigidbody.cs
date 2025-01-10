using UnityEngine;

namespace PFVR.OurPhysics {
    public sealed class KinematicRigidbody : MonoBehaviour {
        [HideInInspector]
        public float mass = 1;
        [HideInInspector]
        public Vector3 velocity = Vector3.zero;
        [SerializeField]
        public bool transferForce = true;

        void Start() {
            if (TryGetComponent<Rigidbody>(out var body)) {
                mass = body.mass;
                velocity = body.velocity;
                body.isKinematic = true;
            }
        }

        void FixedUpdate() {
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        void OnCollisionEnter(Collision collision) {
            if (transferForce && collision.rigidbody != null) {
                collision.rigidbody.AddForce(velocity * mass, ForceMode.Impulse);
            }
        }
    }
}