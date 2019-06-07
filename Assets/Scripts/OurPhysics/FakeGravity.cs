using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Rigidbody))]
    public class FakeGravity : MonoBehaviour {
        [SerializeField]
        [Range(0,10)]
        private float gravity = default;

        void FixedUpdate() {
            GetComponent<Rigidbody>().AddForce(Physics.gravity * gravity, ForceMode.Acceleration);
        }
    }
}