using System;
using UnityEngine.Events;

namespace PFVR.Events {
    /// <summary>
    /// A generic function call. Used by <see cref="GameEventListener"/>.
    /// </summary>
    [Serializable]
    public sealed class GameEventResponse : UnityEvent<GameEvent> {
    }
}