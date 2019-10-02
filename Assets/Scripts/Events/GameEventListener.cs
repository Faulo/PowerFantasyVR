using System;
using UnityEngine;
using UnityEngine.Events;

namespace PFVR.Events {
    /// <summary>
    /// Event listeners will call <see cref="GameEventResponse"/> whenever their <see cref="GameEventType"/> happens.
    /// </summary>
    public class GameEventListener : MonoBehaviour {
        [SerializeField]
        private GameEventType trigger = default;
        [SerializeField]
        private GameEventResponse response = default;
        private void OnEnable() {
            trigger.AddEventListener(response);
        }
        private void OnDisable() {
            trigger.RemoveEventListener(response);
        }
    }
}