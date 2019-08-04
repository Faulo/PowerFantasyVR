using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Player {
    public class CollectableScript : MonoBehaviour {
        void OnTriggerEnter(Collider collider) {
            var connector = collider.gameObject.GetComponentInParent<PlayerBehaviour>();
            if (connector) {
                //TODO: add to highscore or somesuch
                Destroy(gameObject);
            }
        }
    }

}