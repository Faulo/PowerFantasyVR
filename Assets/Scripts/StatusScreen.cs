using PFVR.DataModels;
using PFVR.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace PFVR {
    public class StatusScreen : MonoBehaviour {
        private TextMeshPro mesh;
        private string format;
        private Dictionary<string, Quaternion> args = new Dictionary<string, Quaternion>();

        [SerializeField]
        private int roundingPrecision = 10;

        // Start is called before the first frame update
        void Start() {

            mesh = GetComponent<TextMeshPro>();
            format = mesh.text;
            args["leftGlove"] = default;
            args["leftTracker"] = default;
            args["leftDelta"] = default;
            args["rightGlove"] = default;
            args["rightTracker"] = default;
            args["rightDelta"] = default;

            ManusConnector.onLeftGloveData += (GloveData glove) => {
                args["leftGlove"] = glove.wrist;
                args["leftTracker"] = SteamConnector.leftTracker.rotation;
            };
            ManusConnector.onRightGloveData += (GloveData glove) => {
                args["rightGlove"] = glove.wrist;
                args["leftTracker"] = SteamConnector.rightTracker.rotation;
            };
        }

        // Update is called once per frame
        void Update() {
            args["leftDelta"] = Difference(args["leftTracker"], args["leftGlove"]);
            args["rightDelta"] = Difference(args["rightTracker"], args["rightGlove"]);

            mesh.text = string.Format(
                format,
                args.Values
                    .Select(Round)
                    .Select(Normalize)
                    .Select(Format)
                    .ToArray()
            );
        }

        private Vector3 Difference(Vector3 a, Vector3 b) {
            return a - b;
        }
        private Quaternion Difference(Quaternion a, Quaternion b) {
            return Quaternion.Inverse(a) * b;
        }
        private Vector3Int Round(Vector3 input) {
            return new Vector3Int(Convert.ToInt32(input.x / roundingPrecision) * roundingPrecision, Convert.ToInt32(input.y / roundingPrecision) * roundingPrecision, Convert.ToInt32(input.z / roundingPrecision) * roundingPrecision);
        }
        private Quaternion Round(Quaternion input) {
            return input;
        }
        private Vector3Int Normalize(Vector3Int input) {
            return new Vector3Int((input.x + 720) % 360, (input.y + 720) % 360, (input.z + 720) % 360);
        }
        private Quaternion Normalize(Quaternion input) {
            return input;
        }
        private string Format(Vector3Int input) {
            return string.Format("({0,03},{1,03},{2,03})", input.x, input.y, input.z).Replace(" ", "0");
        }
        private string Format(Quaternion input) {
            return string.Format("({0:0.00},{1:0.00},{2:0.00},{3:0.00})", input.x, input.y, input.z, input.w).Replace(" ", "0");
        }
    }
}
