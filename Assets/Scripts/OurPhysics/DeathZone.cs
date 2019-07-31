using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class DeathZone : MonoBehaviour {
        void OnTriggerExit(Collider collider) {
            //Debug.Log(collider.gameObject.name + " hit the death zone!");
            Destroy(collider.gameObject);
        }
    }
}