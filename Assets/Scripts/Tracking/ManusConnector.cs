using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Tracking {
    public class ManusConnector : MonoBehaviour {
        public OneHand leftHand { get; private set; }
        public OneHand rightHand { get; private set; }

        public delegate void NewHandData(OneHand hand);

        public event NewHandData onLeftHandData;
        public event NewHandData onRightHandData;

        private int playerId = 1;
        
        void Start() {
            leftHand = default;
            rightHand = default;
        }
        void Update() {
            OneHand hand;
            if (TryToFetch(device_type_t.GLOVE_LEFT, out hand)) {
                leftHand = hand;
                onLeftHandData(leftHand);
            }
            if (TryToFetch(device_type_t.GLOVE_RIGHT, out hand)) {
                rightHand = hand;
                onRightHandData(rightHand);
            }
        }
        private bool TryToFetch(device_type_t gloveType, out OneHand outputHand) {
            if (HandDataManager.CanGetHandData(playerId, gloveType)) {
                var data = HandDataManager.GetHandData(playerId, gloveType);
                outputHand = new OneHand {
                    PinkyProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Pinky].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    PinkyMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Pinky].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    RingProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Ring].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    RingMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Ring].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    MiddleProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    MiddleMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    IndexProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Index].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    IndexMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Index].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    ThumbProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Thumb].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    ThumbMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Thumb].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                };
                return true;
            } else {
                outputHand = null;
                return false;
            }
        }
    }
}