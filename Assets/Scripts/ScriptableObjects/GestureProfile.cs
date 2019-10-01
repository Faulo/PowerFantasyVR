using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    /// <summary>
    /// A serializable tuple of <see cref="GestureSet"/> and tracking data, to easily switch which tracking data is used in-game.
    /// 
    /// Its name will be used for the names of the tracking data .csv and .zip files.
    /// </summary>
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