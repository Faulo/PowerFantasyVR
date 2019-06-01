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
        //private SteamConnector steam;
        
        void Start() {
            leftHand = default;
            rightHand = default;
            //steam = FindObjectOfType<SteamConnector>();
        }
        void Update() {
            if (TryToFetch(device_type_t.GLOVE_LEFT, out var hand)) {
                leftHand = hand;
                onLeftHandData?.Invoke(leftHand);
            }
            if (TryToFetch(device_type_t.GLOVE_RIGHT, out hand)) {
                rightHand = hand;
                onRightHandData?.Invoke(rightHand);
            }
        }
        private bool TryToFetch(device_type_t gloveType, out OneHand outputHand) {
            if (HandDataManager.CanGetHandData(playerId, gloveType)) {
                var data = HandDataManager.GetHandData(playerId, gloveType);
                //var rotation = data.processedWristImu.eulerAngles;
                //Debug.Log(tracker.transform.rotation.eulerAngles + " x " + data.processedWristImu.eulerAngles + " = " + rotation);

                outputHand = new OneHand {
                    pinkyProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Pinky].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    pinkyMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Pinky].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    ringProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Ring].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    ringMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Ring].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    middleProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    middleMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    indexProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Index].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    indexMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Index].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    thumbProximal = (float)data.fingers[(int)ApolloHandData.FingerName.Thumb].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Proximal],
                    thumbMedial = (float)data.fingers[(int)ApolloHandData.FingerName.Thumb].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                    //wristX = rotation.x,
                    //wristY = rotation.y,
                    //wristZ = rotation.z
                };
                return true;
            } else {
                outputHand = null;
                return false;
            }
        }
    }
}