using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    /// <summary>
    /// A serializable set of <see cref="Gesture"/>s, to store which of them are active in-game.
    /// </summary>
    [CreateAssetMenu(fileName = "New Gesture Set", menuName = "Gameplay/Gesture Set", order = 2)]
    public class GestureSet : ScriptableObject, IGestureDictionary {
        [SerializeField, HideInInspector]
        Gesture[] gestures = new Gesture[0];

        public IEnumerable<Gesture> gestureObjects => gestures;
        public IEnumerable<string> gestureNames => gestureObjects
            .Select(gesture => gesture.name);

        public Gesture this[string name] {
            get {
                return gestures
                    .Where(gesture => gesture.name == name)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<Gesture> possibleGestures => Resources.LoadAll<Gesture>("ScriptableObjects");
        public bool HasGesture(Gesture gesture) => gestures.Contains(gesture);
        public void AddGesture(Gesture gesture) {
            if (!HasGesture(gesture)) {
                var list = gestures.ToList();
                list.Add(gesture);
                gestures = list.ToArray();
            }
        }
        public void RemoveGesture(Gesture gesture) {
            if (HasGesture(gesture)) {
                var list = gestures.ToList();
                list.Remove(gesture);
                gestures = list.ToArray();
            }
        }
    }
}