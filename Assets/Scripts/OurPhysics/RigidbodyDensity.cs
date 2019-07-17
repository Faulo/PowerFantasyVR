using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Rigidbody)), ExecuteInEditMode]
    public class RigidbodyDensity : MonoBehaviour {
        [SerializeField, Range(0, 100)]
        private float density = 1;
        private float volume => transform.localScale.x * transform.localScale.y * transform.localScale.z;

        void Start() {
            GetComponent<Rigidbody>().mass = volume * density;
        }
    }
}
