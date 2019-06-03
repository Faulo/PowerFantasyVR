using System.Collections;
using TMPro;
using UnityEngine;

namespace PFVR {
    public class DebugScreen : MonoBehaviour {
        private TextMeshPro mesh;

        string myLog;
        Queue myLogQueue = new Queue();

        // Start is called before the first frame update
        void Start() {
            mesh = GetComponent<TextMeshPro>();
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