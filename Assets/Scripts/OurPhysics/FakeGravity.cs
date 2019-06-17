using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class FakeGravity : MonoBehaviour {
        [SerializeField]
        [Range(-10,10)]
        private float gravity = default;

        void FixedUpdate() {
            GetComponents<Rigidbody>()
                .ForAll(body => body.velocity += Physics.gravity * gravity * Time.fixedDeltaTime);
            GetComponents<KinematicRigidbody>()
                .ForAll(body => body.velocity += Physics.gravity * gravity * Time.fixedDeltaTime);
        }
    }
}