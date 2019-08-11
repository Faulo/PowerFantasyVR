using PFVR.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PFVR.Events {
    [Serializable]
    public class GameEventResponse : UnityEvent<GameEvent> {
    }
}