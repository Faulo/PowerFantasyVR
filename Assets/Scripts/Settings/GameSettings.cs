using PFVR.Player;
using PFVR.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PFVR.Settings {
    [CreateAssetMenu(fileName = "Settings", menuName = "Gameplay/Settings", order = 100)]
    public class GameSettings : ScriptableObject {
        public static GameSettings instance => GameManager.instance.settings;

        [Header("Player Settings")]
        public InterfaceType interfaceType;

        [Header("Level Settings")]
        public SceneAsset mainMenu;
        public Level[] levels;
    }
}