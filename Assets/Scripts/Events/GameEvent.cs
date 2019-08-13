using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Events {
    public class GameEvent {
        public GameEventType type { get; internal set; }
        public GameEventSource source { get; internal set; }
        public float timestamp { get; internal set; }
    }
}
