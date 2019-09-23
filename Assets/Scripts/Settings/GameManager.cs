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
                if (SceneManager.GetActiveScene().path == settings.mainMenu.ScenePath) {
                    Application.Quit();
                } else {
                    LoadScene(settings.mainMenu);
                }
            }
        }
        private void LoadScene(SceneReference scene) => SceneManager.LoadScene(scene.ScenePath);
        public void LoadLevel(Level level) => LoadScene(level.scene);
    }
}