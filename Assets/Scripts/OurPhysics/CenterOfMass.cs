using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class CenterOfMass : MonoBehaviour {
        [SerializeField]
        private Rigidbody targetRigidbody = default;
        [SerializeField, Range(0, 100)]
        private float density = 1;
        void Awake() {
            if (targetRigidbody) {
                targetRigidbody.centerOfMass = transform.position - targetRigidbody.transform.position - transform.up;
                targetRigidbody.mass = density * targetRigidbody.transform.localScale.magnitude;
            }
        }
    }
}
