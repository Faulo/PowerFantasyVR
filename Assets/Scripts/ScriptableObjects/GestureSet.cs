using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    /// <summary>
    /// A serializable set of <see cref="Gesture"/>s, to store which of them are active in-game.
    /// </summary>
    [CreateAssetMenu(fileName = "New Gesture Set", menuName = "Gameplay/Gesture Set", order = 2)]
    public class GestureSet : ScriptableObject {
        [Serializable]
        public class GestureDictionary : SerializableDictionary<Gesture, bool> { }

        [SerializeField, HideInInspector]
        private Gesture[] gestures = new Gesture[0];

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
        
        public bool Contains(Gesture gesture) => gestures.Contains(gesture);
        public void Append(Gesture gesture) {
            if (!Contains(gesture)) {
                var list = gestures.ToList();
                list.Add(gesture);
                gestures = list.ToArray();
            }
        }
        public void Remove(Gesture gesture) {
            if (Contains(gesture)) {
                var list = gestures.ToList();
                list.Remove(gesture);
                gestures = list.ToArray();
            }
        }
    }
}