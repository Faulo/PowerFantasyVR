using System;
using System.Collections.Generic;
using System.Linq;
using PFVR.DataModels;
using PFVR.Player;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace PFVR.Backend {
    /// <summary>
    /// A widget for displaying raw Manus glove data. Requires a working <see cref="ManusConnector"/>.
    /// </summary>
    public class StatusScreen : MonoBehaviour {
        TextMeshProUGUI mesh;
        string format;
        Dictionary<string, Vector3> args = new Dictionary<string, Vector3>();

        [SerializeField]
        int roundingPrecision = 10;

        // Start is called before the first frame update
        void Start() {

            mesh = GetComponent<TextMeshProUGUI>();
            format = mesh.text;
            args["leftGloveRotation"] = default;
            args["leftTrackerRotation"] = default;
            args["leftTrackerPosition"] = default;
            args["rightGloveRotation"] = default;
            args["rightTrackerRotation"] = default;
            args["rightTrackerPosition"] = default;

            ManusConnector.onLeftGloveData += (GloveData glove) => {
                args["leftGloveRotation"] = glove.wrist.eulerAngles;
                args["leftTrackerRotation"] = SteamConnector.leftTracker.rotation.eulerAngles;
                args["leftTrackerPosition"] = SteamConnector.leftTracker.position;
            };
            ManusConnector.onRightGloveData += (GloveData glove) => {
                args["rightGloveRotation"] = glove.wrist.eulerAngles;
                args["rightTrackerRotation"] = SteamConnector.rightTracker.rotation.eulerAngles;
                args["rightTrackerPosition"] = SteamConnector.rightTracker.position;
            };

            XRSettings.LoadDeviceByName("OpenVR");
            SteamVR.Initialize(true);
        }

        // Update is called once per frame
        void Update() {
            mesh.text = string.Format(
                format,
                args.Values
                    .Select(Round)
                    .Select(Normalize)
                    .Select(Format)
                    .ToArray()
            );
        }

        Vector3 Difference(Vector3 a, Vector3 b) {
            return a - b;
        }
        Quaternion Difference(Quaternion a, Quaternion b) {
            return Quaternion.Inverse(a) * b;
        }
        Vector3Int Round(Vector3 input) {
            return new Vector3Int(Convert.ToInt32(input.x / roundingPrecision) * roundingPrecision, Convert.ToInt32(input.y / roundingPrecision) * roundingPrecision, Convert.ToInt32(input.z / roundingPrecision) * roundingPrecision);
        }
        Quaternion Round(Quaternion input) {
            return input;
        }
        Vector3Int Normalize(Vector3Int input) {
            return new Vector3Int((input.x + 720) % 360, (input.y + 720) % 360, (input.z + 720) % 360);
        }
        Quaternion Normalize(Quaternion input) {
            return input;
        }
        string Format(Vector3Int input) {
            return string.Format("({0,03},{1,03},{2,03})", input.x, input.y, input.z).Replace(" ", "0");
        }
        string Format(Quaternion input) {
            return string.Format("({0:0.00},{1:0.00},{2:0.00},{3:0.00})", input.x, input.y, input.z, input.w).Replace(" ", "0");
        }
    }
}
