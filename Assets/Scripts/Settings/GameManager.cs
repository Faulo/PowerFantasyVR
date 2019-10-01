using System;
using PFVR.Player;
using PFVR.ScriptableObjects;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PFVR.Settings {
    /// <summary>
    /// Some global keybindings and level load code.
    /// </summary>
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

        private bool IsInMainMenu() {
            return SceneManager.GetActiveScene().path == settings.mainMenu.ScenePath;
        }

        public void Exit() {
            Application.Quit();
        }

        public void TakeScreenshot() {
            var name = string.Format(settings.screenshotPath, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
            ScreenCapture.CaptureScreenshot(name, 4);
            Debug.Log("Screenshot '" + name + "' done!");
        }

        private void LoadScene(SceneReference scene) => SceneManager.LoadScene(scene.ScenePath);
        public void LoadLevel(Level level) => LoadScene(level.scene);
        public void LoadMainMenu() => LoadScene(settings.mainMenu);
    }
}