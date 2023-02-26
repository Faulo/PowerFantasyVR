using System.Collections.Generic;
using Slothsoft.UnityExtensions;

namespace PFVR.Events {
    /// <summary>
    /// All the different kinds of events.
    /// </summary>
    public enum GameEventType {
        PlayerHpChanged,
        PowerUpCollected,
        CoinCollected
    }
    public static class GameEventTypeExtensions {
        static IDictionary<GameEventType, ISet<GameEventResponse>> listeners = new Dictionary<GameEventType, ISet<GameEventResponse>>();
        public static void AddEventListener(this GameEventType type, GameEventResponse response) {
            if (!listeners.ContainsKey(type)) {
                listeners[type] = new HashSet<GameEventResponse>();
            }
            listeners[type].Add(response);
        }
        public static void RemoveEventListener(this GameEventType type, GameEventResponse response) {
            listeners[type].Remove(response);
        }
        public static void DispatchEvent(this GameEventType type, GameEvent gameEvent) {
            if (listeners.ContainsKey(gameEvent.type)) {
                listeners[gameEvent.type]
                    .ForAll(response => response.Invoke(gameEvent));
            }
        }
    }
}