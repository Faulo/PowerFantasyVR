using System.Collections.Generic;

namespace PFVR.ScriptableObjects {
    public interface IGestureDictionary {
        IEnumerable<Gesture> possibleGestures { get; }
        bool HasGesture(Gesture gesture);
        void AddGesture(Gesture gesture);
        void RemoveGesture(Gesture gesture);
    }
}
