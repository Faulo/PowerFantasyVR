using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PFVR.ScriptableObjects;
using Slothsoft.UnityExtensions;

namespace PFVR.Player {
    public class PowerUp : MonoBehaviour {
        [SerializeField]
        private Gesture gestureToUnlock = default;
        [SerializeField]
        private MeshRenderer[] meshesToColorize = default;

        // Start is called before the first frame update
        void Start() {
            if (gestureToUnlock) {
                meshesToColorize.ForAll(meshRenderer => meshRenderer.material.SetColor("_EmissionColor", gestureToUnlock.spellColor));
            }
        }

        void OnTriggerEnter(Collider collider) {
            var connector = collider.gameObject.GetComponentInParent<GestureConnector>();
            if (connector) {
                connector.Unlock(gestureToUnlock);
                Destroy(gameObject);
            }
        }
    }
}
