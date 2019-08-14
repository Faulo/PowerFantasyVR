using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class DeathZone : MonoBehaviour {
        private Bounds bounds;
        private void Awake() {
            bounds = GetComponent<BoxCollider>().bounds;
        }
        void OnTriggerExit(Collider collider) {
            if (!bounds.Contains(collider.transform.position)) {
                //Debug.Log(collider.gameObject.name + " hit the death zone at " + collider.transform.position + "!");
                Destroy(collider.gameObject);
            }
        }
    }
}