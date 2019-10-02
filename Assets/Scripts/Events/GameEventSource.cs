using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Events {
    /// <summary>
    /// A generic function call. Used by <see cref="GameEventListener"/>.
    /// </summary>
    public class GameEventSource : MonoBehaviour {
        public void Raise(GameEventType type) {
            type.DispatchEvent(new GameEvent() { type = type, source = this, timestamp = Time.time });
        }
    }
}