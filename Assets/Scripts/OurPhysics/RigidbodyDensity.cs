using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Rigidbody)), ExecuteInEditMode]
    public class RigidbodyDensity : MonoBehaviour {
        [SerializeField, Range(0, 100)]
        private float density = 1;
        private float volume => transform.localScale.x * transform.localScale.y * transform.localScale.z;

        void Update() {
            GetComponent<Rigidbody>().mass = volume * density;
            GetComponentsInChildren<Collider>().ForAll(collider => collider.enabled = true);
            if (Random.value > 0.5f) {
                //Destroy(gameObject);
            }
        }
    }
}
