using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.OurPhysics {
    public sealed class FakeGravity : MonoBehaviour {
        [SerializeField]
        [Range(-10, 10)]
        float gravity = default;

        void FixedUpdate() {
            GetComponents<Rigidbody>()
                .ForAll(body => body.velocity += gravity * Time.deltaTime * Physics.gravity);
            GetComponents<KinematicRigidbody>()
                .ForAll(body => body.velocity += gravity * Time.deltaTime * Physics.gravity);
        }
    }
}