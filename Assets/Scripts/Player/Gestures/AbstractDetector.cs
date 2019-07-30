using PFVR.ScriptableObjects;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace PFVR.Player.Gestures {
    public class AbstractDetector : MonoBehaviour {
        [SerializeField]
        private Gesture triggeredGesture = default;

        private IEnumerable<GestureConnector> connectors;

        // Start is called before the first frame update
        void Start() {
            connectors = FindObjectsOfType<GestureConnector>();
        }

        // Update is called once per frame
        protected void TurnOn() {
            connectors.ForAll(connector => connector.StartComplexGesture(triggeredGesture));
        }
        protected void TurnOff() {
            connectors.ForAll(connector => connector.StopComplexGesture(triggeredGesture));
        }
    }
}