using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PFVR.ScriptableObjects;
using PFVR.Events;
using Slothsoft.UnityExtensions;

namespace PFVR.Player {
    [RequireComponent(typeof(GameEventSource))]
    public class PowerUp : MonoBehaviour {
        [SerializeField]
        private Gesture gestureToUnlock = default;
        [SerializeField]
        private MeshRenderer[] meshesToColorize = default;

        private GameEventSource eventTarget;

        // Start is called before the first frame update
        void Start() {
            eventTarget = GetComponent<GameEventSource>();
            if (gestureToUnlock) {
                meshesToColorize.ForAll(meshRenderer => meshRenderer.material.SetColor("_EmissionColor", gestureToUnlock.spellColor));
            }
        }

        void OnTriggerEnter(Collider collider) {
            var connector = collider.gameObject.GetComponentInParent<GestureConnector>();
            if (connector) {
                connector.Unlock(gestureToUnlock);
                eventTarget.Raise(GameEventType.PowerUpCollected);
                Destroy(gameObject);
            }
        }
    }
}
