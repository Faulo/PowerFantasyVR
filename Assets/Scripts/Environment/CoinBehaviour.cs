using PFVR.Events;
using PFVR.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Environment {
    /// <summary>
    /// Basic collectible.
    /// </summary>
    public class CoinBehaviour : MonoBehaviour {
        [SerializeField]
        private LayerMask collectingLayer = default;
        [SerializeField]
        private GameEventSource eventSource = default;

        void OnTriggerEnter(Collider other) {
            if (((1 << other.gameObject.layer) & collectingLayer) != 0) {
                eventSource.Raise(GameEventType.CoinCollected);
                Destroy(eventSource.gameObject);
            }
        }
    }
}