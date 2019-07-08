using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Gesture Set", menuName = "Gameplay/Gesture Set", order = 2)]
    public class GestureSet : ScriptableObject {
        [SerializeField]
        private DefaultAsset modelFile = default;
        
        [SerializeField]
        private Gesture[] gestures = default;

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

        public IEnumerable<Gesture> gestureObjects => gestures;
        public IEnumerable<string> gestureNames => gestures.Select(gesture => gesture.name);

        public Gesture this[string name] {
            get {
                return gestures
                    .Where(gesture => gesture.name == name)
                    .FirstOrDefault();
            }
        }
    }
}