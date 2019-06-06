using PFVR.ScriptableObjects;
using PFVR.Tracking;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace PFVR.Debugging {
    public class RecognizerScreen : MonoBehaviour {
        private TextMeshPro mesh;
        private string format;

        private Dictionary<string, Gesture> args = new Dictionary<string, Gesture>();

        // Start is called before the first frame update
        void Start() {
            mesh = GetComponent<TextMeshPro>();
            format = mesh.text;

            args["left"] = null;
            args["right"] = null;

            GestureConnector.onLeftGesture += (Gesture gesture) => {
                args["left"] = gesture;
            };
            GestureConnector.onRightGesture += (Gesture gesture) => {
                args["right"] = gesture;
            };
        }

        // Update is called once per frame
        void Update() {
            mesh.text = string.Format(
                format,
                args.Values
                    .Select(Format)
                    .ToArray()
            );
        }
        string Format(Gesture gesture) {
            return gesture == null
                ? "???"
                : gesture.ToString();
        }
    }
}