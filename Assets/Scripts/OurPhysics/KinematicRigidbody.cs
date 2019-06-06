using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicRigidbody : MonoBehaviour {
        public Vector3 velocity = Vector3.zero;

        void FixedUpdate() {
            transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
        }
    }
}