using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Events {
    public class GameEventTarget : MonoBehaviour {
        private IDictionary<GameEventType, ISet<GameEventResponse>> listeners = new Dictionary<GameEventType, ISet<GameEventResponse>>();
        public void AddEventListener(GameEventType type, GameEventResponse response) {
            if (!listeners.ContainsKey(type)) {
                listeners[type] = new HashSet<GameEventResponse>();
            }
            listeners[type].Add(response);
        }
        public void RemoveEventListener(GameEventType type, GameEventResponse response) {
            listeners[type].Remove(response);
        }
        public void Raise(GameEventType type) {
            DispatchEvent(new GameEvent() { type = type, target = this, timestamp = Time.time });
        }
        private void DispatchEvent(GameEvent gameEvent) {
            if (listeners.ContainsKey(gameEvent.type)) {
                listeners[gameEvent.type].ForAll(response => response.Invoke(gameEvent));
            }
        }
    }
}