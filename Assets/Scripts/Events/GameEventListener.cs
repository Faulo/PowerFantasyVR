using System;
using UnityEngine;
using UnityEngine.Events;

namespace PFVR.Events {
    public class GameEventListener : MonoBehaviour {
        [SerializeField]
        private GameEventTarget target = default;
        [SerializeField]
        private GameEventType trigger = default;
        [SerializeField]
        private GameEventResponse response = default;
        private void OnEnable() {
            target?.AddEventListener(trigger, response);
        }
        private void OnDisable() {
            target?.RemoveEventListener(trigger, response);
        }
    }
}