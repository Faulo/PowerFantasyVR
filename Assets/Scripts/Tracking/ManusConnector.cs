using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Tracking {

    public static class ManusConnector {
        private static int playerId = 0;
        public static OneHand leftHand {
            get {
                if (playerId == 0) {
                    HandDataManager.GetPlayerNumber(out playerId);
                }
                    if (HandDataManager.CanGetHandData(playerId, device_type_t.GLOVE_LEFT)) {
                        ApolloHandData apolloHandData = HandDataManager.GetHandData(playerId, device_type_t.GLOVE_LEFT);
                        return new OneHand {
                            Kleiner = (float) apolloHandData.fingers[(int)ApolloHandData.FingerName.Pinky].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                            Ring = (float) apolloHandData.fingers[(int)ApolloHandData.FingerName.Ring].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                            Mittel = (float) apolloHandData.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                            Zeige = (float) apolloHandData.fingers[(int)ApolloHandData.FingerName.Index].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial],
                            Daumen = (float) apolloHandData.fingers[(int)ApolloHandData.FingerName.Thumb].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial]
                        };
                    }
                return null;
            }
        }
    }

}