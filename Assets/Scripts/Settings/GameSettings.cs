using PFVR.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PFVR.Settings {
    [CreateAssetMenu(fileName = "Settings", menuName = "Gameplay/Settings", order = 100)]
    public class GameSettings : ScriptableObject {
        public static GameSettings instance {
            get {
                if (instanceCache == null) {
                    instanceCache = FindObjectOfType<GameManager>().settings;
                }
                return instanceCache;
            }
        }
        private static GameSettings instanceCache;

        [Header("Player Settings")]
        public InterfaceType interfaceType;

        [Header("Level Settings")]
        public SceneAsset mainMenu;
        public SceneAsset[] levels;

        
    }
}