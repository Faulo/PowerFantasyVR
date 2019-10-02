using PFVR.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PFVR.Events {
    /// <summary>
    /// A generic function call. Used by <see cref="GameEventListener"/>.
    /// </summary>
    [Serializable]
    public class GameEventResponse : UnityEvent<GameEvent> {
    }
}