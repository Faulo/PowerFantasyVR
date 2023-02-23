using System.Collections.Generic;
using System.Linq;
using PFVR.Player;
using PFVR.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace PFVR.Backend {
    /// <summary>
    /// A widget for displaying recognized gestures. Requires a working <see cref="GestureConnector"/>.
    /// </summary>
    public class RecognizerScreen : MonoBehaviour {
        TextMeshProUGUI mesh;
        string format;

        Dictionary<string, IEnumerable<Gesture>> args = new Dictionary<string, IEnumerable<Gesture>>();

        // Start is called before the first frame update
        void Start() {
            mesh = GetComponent<TextMeshProUGUI>();
            format = mesh.text;

            args["available"] = GestureConnector.instance.availableGestures;
            args["left"] = null;
            args["right"] = null;

            GestureConnector.onLeftGesture += (Gesture gesture) => {
                args["left"] = new Gesture[] { gesture };
            };
            GestureConnector.onRightGesture += (Gesture gesture) => {
                args["right"] = new Gesture[] { gesture };
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
        string Format(IEnumerable<Gesture> gestures) {
            return gestures == null
                ? "???"
                : string.Join(", ", gestures.Select(Format));
        }
        string Format(Gesture gesture) {
            return gesture == null
                ? "???"
                : gesture.name;
        }
    }
}