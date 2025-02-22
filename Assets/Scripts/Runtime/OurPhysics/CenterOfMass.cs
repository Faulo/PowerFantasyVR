﻿using UnityEngine;

namespace PFVR.OurPhysics {
    public sealed class CenterOfMass : MonoBehaviour {
        [SerializeField]
        Rigidbody targetRigidbody = default;
        void Start() {
            if (targetRigidbody) {
                targetRigidbody.centerOfMass = transform.position - targetRigidbody.transform.position - transform.up;
            }
        }
    }
}
