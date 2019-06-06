using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Rigidbody))]
    public class FakeGravity : MonoBehaviour {
        [SerializeField]
        [Range(0,1)]
        private float gravity;

        void FixedUpdate() {
            GetComponent<Rigidbody>().AddForce(Physics.gravity * gravity, ForceMode.Acceleration);
        }
    }
}