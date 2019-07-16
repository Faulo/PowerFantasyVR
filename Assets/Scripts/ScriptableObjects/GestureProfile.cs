using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "GestureProfile.Player", menuName = "Gameplay/Gesture Profile", order = 3)]
    public class GestureProfile : ScriptableObject {
        [SerializeField]
        public GestureSet gestureSet = default;

        public string trackingDataPath {
            get {
                return "Assets/Resources/TrackingData/" + name + ".csv";
            }
        }
        public string modelDataPath {
            get {
                return "Assets/StreamingAssets/DataModels/" + name + ".zip";
            }
        }
    }
}