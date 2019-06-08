using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using PFVR.DataModels;
using UnityEngine;

namespace PFVR.Player {
    public class ManusConnector : MonoBehaviour {
        public delegate void NewGloveData(GloveData hand);

        public static event NewGloveData onLeftGloveData;
        public static event NewGloveData onRightGloveData;

        private int playerId = 1;

        void Start() {
        }
        void Update() {
            if (onLeftGloveData != null && TryToFetch(device_type_t.GLOVE_LEFT, GloveLaterality.GLOVE_LEFT, SteamConnector.leftTracker, out var glove)) {
                onLeftGloveData(glove);

            }
            if (onRightGloveData != null && TryToFetch(device_type_t.GLOVE_RIGHT, GloveLaterality.GLOVE_RIGHT, SteamConnector.rightTracker, out glove)) {
                onRightGloveData(glove);

            }
        }

        private bool TryToFetch(device_type_t gloveType, GloveLaterality laterality, Transform tracker, out GloveData output) {
            if (HandDataManager.CanGetHandData(playerId, gloveType)) {
                var data = HandDataManager.GetHandData(playerId, gloveType);

                output = new GloveData {
                    device = gloveType,
                    laterality = laterality,
                    pinkyProximal = data.fingers[(int)ApolloHandData.FingerName.Pinky].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    pinkyMedial = data.fingers[(int)ApolloHandData.FingerName.Pinky].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    ringProximal = data.fingers[(int)ApolloHandData.FingerName.Ring].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    ringMedial = data.fingers[(int)ApolloHandData.FingerName.Ring].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    middleProximal = data.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    middleMedial = data.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    indexProximal = data.fingers[(int)ApolloHandData.FingerName.Index].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    indexMedial = data.fingers[(int)ApolloHandData.FingerName.Index].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    thumbProximal = data.fingers[(int)ApolloHandData.FingerName.Thumb].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    thumbMedial = data.fingers[(int)ApolloHandData.FingerName.Thumb].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    wrist = data.processedWristImu,
                    tracker = tracker,
                };
                return true;
            } else {
                output = null;
                return false;
            }
        }
    }
}