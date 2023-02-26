using UnityEngine;

namespace PFVR.Events {
    /// <summary>
    /// Event listeners will call <see cref="GameEventResponse"/> whenever their <see cref="GameEventType"/> happens.
    /// </summary>
    public class GameEventListener : MonoBehaviour {
        [SerializeField]
        GameEventType trigger = default;
        [SerializeField]
        GameEventResponse response = default;
        void OnEnable() {
            trigger.AddEventListener(response);
        }
        void OnDisable() {
            trigger.RemoveEventListener(response);
        }
    }
}