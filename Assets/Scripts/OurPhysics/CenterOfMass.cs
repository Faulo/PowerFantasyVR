using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class CenterOfMass : MonoBehaviour {
        [SerializeField]
        private Rigidbody targetRigidbody = default;
        void Start() {
            if (targetRigidbody) {
                targetRigidbody.centerOfMass = transform.position - targetRigidbody.transform.position - transform.up;
            }
        }
    }
}
