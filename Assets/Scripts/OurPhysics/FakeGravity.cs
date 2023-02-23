using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class FakeGravity : MonoBehaviour {
        [SerializeField]
        [Range(-10, 10)]
        float gravity = default;

        void FixedUpdate() {
            GetComponents<Rigidbody>()
                .ForAll(body => body.velocity += Physics.gravity * gravity * Time.deltaTime);
            GetComponents<KinematicRigidbody>()
                .ForAll(body => body.velocity += Physics.gravity * gravity * Time.deltaTime);
        }
    }
}