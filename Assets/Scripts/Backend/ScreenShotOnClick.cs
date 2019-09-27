using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Backend {
    /// <summary>
    /// Adds a screenshot functionality to any scene.
    /// </summary>
    public class ScreenShotOnClick : MonoBehaviour {
        [SerializeField]
        private KeyCode hotKey = KeyCode.F12;
        private string fileName = "Temp/Screenshot {0}.png";

        void Update() {
            if (Input.GetKeyDown(hotKey)) {
                var name = string.Format(fileName, System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                ScreenCapture.CaptureScreenshot(name, 4);
                Debug.Log("Screenshot '" + name + "' done!");
            }
        }
    }
}
