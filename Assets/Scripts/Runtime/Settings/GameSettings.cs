using PFVR.Player;
using PFVR.ScriptableObjects;
using UnityEngine;

namespace PFVR.Settings {
    /// <summary>
    /// Basic game parameters, like which scenes are to be used.
    /// </summary>
    [CreateAssetMenu(fileName = "Settings", menuName = "Gameplay/Settings", order = 100)]
    public class GameSettings : ScriptableObject {
        public static GameSettings instance => GameManager.instance.settings;
        [Header("Special Keybindings")]
        public KeyCode menuKey = KeyCode.Escape;
        public KeyCode screenshotKey = KeyCode.F12;

        [Header("Player Settings")]
        public InterfaceType interfaceType;

        [Header("Level Settings")]
        public SceneReference mainMenu;
        public Level[] levels;

        [Header("Misc Settings")]
        public string screenshotPath = "Temp/Screenshot {0}.png";
    }
}