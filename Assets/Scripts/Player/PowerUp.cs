using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PFVR.ScriptableObjects;

namespace PFVR.Player {
    public class PowerUp : MonoBehaviour {
        [SerializeField]
        private Gesture gestureToUnlock = default;

        // Start is called before the first frame update
        void Start() {
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", gestureToUnlock.spellColor);
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
