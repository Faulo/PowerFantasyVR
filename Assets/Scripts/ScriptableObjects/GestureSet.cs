using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Gesture Set", menuName = "Gameplay/Gesture Set", order = 2)]
    public class GestureSet : ScriptableObject {
        [Serializable]
        public class GestureDictionary : SerializableDictionary<Gesture, bool> {}

        [SerializeField]
        private DefaultAsset modelFile = default;

        [SerializeField, HideInInspector]
        public GestureDictionary activeGestures = new GestureDictionary();

        public string modelPath {
            get {
                return AssetDatabase.GetAssetPath(modelFile);
            }
        }

        public string trackingDataPath {
            get {
                return "Assets/Resources/TrackingData/" + name + ".csv";
            }
        }

        public IEnumerable<Gesture> gestureObjects => activeGestures
            .Where(keyval => keyval.Value)
            .Select(keyval => keyval.Key);
        public IEnumerable<string> gestureNames => gestureObjects
            .Select(gesture => gesture.name);

        public Gesture this[string name] {
            get {
                return gestureObjects
                    .Where(gesture => gesture.name == name)
                    .FirstOrDefault();
            }
        }

        public bool IsActive(Gesture gesture) {
            return activeGestures.ContainsKey(gesture)
                ? activeGestures[gesture]
                : false;
        }
    }
}