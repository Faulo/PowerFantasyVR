using UnityEngine;

namespace PFVR.Events {
    /// <summary>
    /// A generic function call. Used by <see cref="GameEventListener"/>.
    /// </summary>
    public sealed class GameEventSource : MonoBehaviour {
        public void Raise(GameEventType type) {
            type.DispatchEvent(new GameEvent() { type = type, source = this, timestamp = Time.time });
        }
    }
}