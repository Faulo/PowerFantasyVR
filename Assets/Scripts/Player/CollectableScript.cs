using PFVR.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Player {
    public class CollectableScript : MonoBehaviour {
        private GameEventTarget eventTarget => GetComponent<GameEventTarget>();
        void OnTriggerEnter(Collider collider) {
            var connector = collider.gameObject.GetComponentInParent<PlayerBehaviour>();
            if (connector) {
                eventTarget.Raise(GameEventType.CoinCollected);
            }
        }
    }
}