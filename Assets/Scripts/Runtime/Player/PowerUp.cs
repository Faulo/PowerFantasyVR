using PFVR.Events;
using PFVR.ScriptableObjects;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.Player {
    [RequireComponent(typeof(GameEventSource))]
    public sealed class PowerUp : MonoBehaviour {
        [SerializeField]
        Gesture gestureToUnlock = default;
        [SerializeField]
        MeshRenderer[] meshesToColorize = default;

        GameEventSource eventTarget;

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
