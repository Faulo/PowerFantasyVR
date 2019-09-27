using System.Collections;
using TMPro;
using UnityEngine;

namespace PFVR.Backend {
    /// <summary>
    /// A widget for displaying log data.
    /// </summary>
    public class DebugScreen : MonoBehaviour {
        private TextMeshProUGUI mesh;

        string myLog;
        Queue myLogQueue = new Queue();

        // Start is called before the first frame update
        void Start() {
            mesh = GetComponent<TextMeshProUGUI>();
            Application.logMessageReceived += HandleLog;
        }

        // Update is called once per frame
        void Update() {
        }
        void HandleLog(string logString, string stackTrace, LogType type) {
            logString = "[" + type + "] : " + logString + "\n";
            mesh.text += logString;
        }
    }
}