﻿using PFVR.Player;
using PFVR.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace PFVR.Backend {
    public class RecognizerScreen : MonoBehaviour {
        private TextMeshProUGUI mesh;
        private string format;

        private Dictionary<string, Gesture> args = new Dictionary<string, Gesture>();

        // Start is called before the first frame update
        void Start() {
            mesh = GetComponent<TextMeshProUGUI>();
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
                : gesture.name;
        }
    }
}