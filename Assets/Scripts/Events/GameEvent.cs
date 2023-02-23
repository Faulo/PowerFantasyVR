namespace PFVR.Events {
    /// <summary>
    /// The model used for the instance of an event.
    /// </summary>
    public class GameEvent {
        public GameEventType type { get; internal set; }
        public GameEventSource source { get; internal set; }
        public float timestamp { get; internal set; }
    }
}
