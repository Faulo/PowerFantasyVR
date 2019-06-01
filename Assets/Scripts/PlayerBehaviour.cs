using ManusVR.Core.Apollo;
using PFVR.DataModels;
using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace PFVR {

    public class PlayerBehaviour : MonoBehaviour {
        public new Rigidbody rigidbody {
            get {
                return GetComponent<Rigidbody>();
            }
        }
        [SerializeField]
        private PlayerHandBehaviour leftHand;

        [SerializeField]
        private PlayerHandBehaviour rightHand;

        private Gesture leftGesture;
        private Gesture rightGesture;

        void Start() {
            leftHand.Init(this, GloveLaterality.GLOVE_LEFT);
            rightHand.Init(this, GloveLaterality.GLOVE_RIGHT);

            GestureConnector.onLeftGesture += (Gesture gesture) => {
                leftHand.gesture = gesture;
            };
            GestureConnector.onRightGesture += (Gesture gesture) => {
                rightHand.gesture = gesture;
            };
        }

        void Update() {
        }
    }

}