using PFVR.Events;
using UnityEngine;

namespace PFVR.Environment {
    /// <summary>
    /// Basic collectible.
    /// </summary>
    public sealed class CoinBehaviour : MonoBehaviour {
        [SerializeField]
        LayerMask collectingLayer = default;
        [SerializeField]
        GameEventSource eventSource = default;

        void OnTriggerEnter(Collider other) {
            if (((1 << other.gameObject.layer) & collectingLayer) != 0) {
                eventSource.Raise(GameEventType.CoinCollected);
                Destroy(eventSource.gameObject);
            }
        }
    }
}