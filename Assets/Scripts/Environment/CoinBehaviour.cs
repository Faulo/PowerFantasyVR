using PFVR.Events;
using PFVR.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Environment {
    public class CoinBehaviour : MonoBehaviour {
        private GameEventSource eventSource => GetComponent<GameEventSource>();
        void OnTriggerEnter(Collider collider) {
            var connector = collider.gameObject.GetComponentInParent<PlayerBehaviour>();
            if (connector) {
                eventSource.Raise(GameEventType.CoinCollected);
                Destroy(gameObject);
            }
        }
    }
}