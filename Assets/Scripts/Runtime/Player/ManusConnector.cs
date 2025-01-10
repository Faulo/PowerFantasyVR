using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using PFVR.DataModels;
using UnityEngine;

namespace PFVR.Player {
    public sealed class ManusConnector : MonoBehaviour {
        public delegate void NewGloveData(GloveData hand);

        public static event NewGloveData onLeftGloveData;
        public static event NewGloveData onRightGloveData;

        [SerializeField]
        bool useRumbleMixer = false;
        [SerializeField, Range(1, 1000)]
        ushort rumbleInterval = 1;

        static RumbleMixer leftRumble;
        static RumbleMixer rightRumble;
        public static void Rumble(GloveLaterality side, ushort duration, float power) {
            switch (side) {
                case GloveLaterality.GLOVE_LEFT:
                    leftRumble.Rumble(duration, power);
                    break;
                case GloveLaterality.GLOVE_RIGHT:
                    rightRumble.Rumble(duration, power);
                    break;
            }
        }

        int playerId = 1;

        void Start() {
            leftRumble = new RumbleMixer(GloveLaterality.GLOVE_LEFT, rumbleInterval, useRumbleMixer);
            rightRumble = new RumbleMixer(GloveLaterality.GLOVE_RIGHT, rumbleInterval, useRumbleMixer);
            StartCoroutine(leftRumble.RumbleMixerRoutine());
            StartCoroutine(rightRumble.RumbleMixerRoutine());
        }
        void FixedUpdate() {
            if (onLeftGloveData != null && TryToFetch(device_type_t.GLOVE_LEFT, GloveLaterality.GLOVE_LEFT, SteamConnector.leftTracker, out var glove)) {
                onLeftGloveData(glove);
            }

            if (onRightGloveData != null && TryToFetch(device_type_t.GLOVE_RIGHT, GloveLaterality.GLOVE_RIGHT, SteamConnector.rightTracker, out glove)) {
                onRightGloveData(glove);
            }
        }

        bool TryToFetch(device_type_t gloveType, GloveLaterality laterality, Transform tracker, out GloveData output) {
            try {
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
                }
            } catch (System.NullReferenceException e) {
                Debug.Log(e);
            }

            output = null;
            return false;
        }
    }
}