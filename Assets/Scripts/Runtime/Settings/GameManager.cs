using System;
using PFVR.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PFVR.Settings {
    /// <summary>
    /// Some global keybindings and level load code.
    /// </summary>
    public sealed class GameManager : MonoBehaviour {
        public static GameManager instance {
            get {
                if (instanceCache == null) {
                    instanceCache = FindObjectOfType<GameManager>();
                }

                return instanceCache;
            }
        }
        static GameManager instanceCache;

        public GameSettings settings;

        void Update() {
            if (Input.GetKeyDown(settings.menuKey)) {
                if (IsInMainMenu()) {
                    Exit();
                } else {
                    LoadMainMenu();
                }
            }

            if (Input.GetKeyDown(settings.screenshotKey)) {
                TakeScreenshot();
            }
        }

        bool IsInMainMenu() {
            return SceneManager.GetActiveScene().path == settings.mainMenu.ScenePath;
        }

        public void Exit() {
            Application.Quit();
        }

        public void TakeScreenshot() {
            string name = string.Format(settings.screenshotPath, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
            ScreenCapture.CaptureScreenshot(name, 4);
            Debug.Log("Screenshot '" + name + "' done!");
        }

        void LoadScene(SceneReference scene) => SceneManager.LoadScene(scene.ScenePath);
        public void LoadLevel(Level level) => LoadScene(level.scene);
        public void LoadMainMenu() => LoadScene(settings.mainMenu);
    }
}