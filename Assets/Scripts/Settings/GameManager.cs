using PFVR.Player;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PFVR.Settings {
    public class GameManager : MonoBehaviour {
        public GameSettings settings;

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                LoadScene(settings.mainMenu);
            }
            if (Input.GetKeyDown(KeyCode.F1)) {
                LoadScene(settings.levels[0]);
            }
            if (Input.GetKeyDown(KeyCode.F2)) {
                LoadScene(settings.levels[1]);
            }
            if (Input.GetKeyDown(KeyCode.F3)) {
                LoadScene(settings.levels[2]);
            }
            if (Input.GetKeyDown(KeyCode.F4)) {
                LoadScene(settings.levels[3]);
            }
        }

        private void LoadScene(SceneAsset scene) => SceneManager.LoadScene(scene.name);

        public void SetInterfaceType(InterfaceType type) {
            settings.interfaceType = type;
        }
        public void SetInterfaceType(TMP_Dropdown dropdown) => SetInterfaceType((InterfaceType)dropdown.value);
    }
}