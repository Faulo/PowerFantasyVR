using PFVR.Player;
using PFVR.ScriptableObjects;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PFVR.Settings {
    public class GameManager : MonoBehaviour {
        public static GameManager instance {
            get {
                if (instanceCache == null) {
                    instanceCache = FindObjectOfType<GameManager>();
                }
                return instanceCache;
            }
        }
        private static GameManager instanceCache;

        public GameSettings settings;

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                LoadScene(settings.mainMenu);
            }
            if (Input.GetKeyDown(KeyCode.F1)) {
                LoadLevel(settings.levels[0]);
            }
            if (Input.GetKeyDown(KeyCode.F2)) {
                LoadLevel(settings.levels[1]);
            }
            if (Input.GetKeyDown(KeyCode.F3)) {
                LoadLevel(settings.levels[2]);
            }
            if (Input.GetKeyDown(KeyCode.F4)) {
                LoadLevel(settings.levels[3]);
            }
        }
        private void LoadScene(SceneAsset scene) => SceneManager.LoadScene(scene.name);
        public void LoadLevel(Level level) => LoadScene(level.scene);
    }
}