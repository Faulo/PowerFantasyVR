using PFVR.Events;
using PFVR.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Environment {
    public class CoinBehaviour : MonoBehaviour {
        [SerializeField]
        private LayerMask collectingLayer = default;
        [SerializeField]
        private GameEventSource eventSource = default;

        void OnTriggerEnter(Collider other) {
            Debug.Log(other + ": " + other.gameObject.layer);
            if (((1 << other.gameObject.layer) & collectingLayer) != 0) {
                eventSource.Raise(GameEventType.CoinCollected);
                Destroy(eventSource.gameObject);
            }
        }
    }
}