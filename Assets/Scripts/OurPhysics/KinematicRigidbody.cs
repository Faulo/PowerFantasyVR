using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicRigidbody : MonoBehaviour {
        public Vector3 velocity = Vector3.zero;

        void FixedUpdate() {
            transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
        }
        private void OnCollisionEnter(Collision collision) {
            var body = collision.rigidbody;
            if (body != null) {
                body.AddForce(velocity * GetComponent<Rigidbody>().mass);
            }
        }
    }
}