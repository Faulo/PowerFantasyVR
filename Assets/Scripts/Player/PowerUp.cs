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

        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerEnter(Collider other) {
            if (other.gameObject.layer == LayerMask.GetMask("Player")) {
                Debug.Log("Neue Fähigkeit verfügbar!");
            }
        }

    }
}
