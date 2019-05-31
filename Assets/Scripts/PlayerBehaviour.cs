using ManusVR.Core.Apollo;
using PFVR.Tracking;
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
        public Hand leftHand { get; private set; }
        public Hand rightHand { get; private set; }

        private Gesture leftGesture;
        private Gesture rightGesture;

        void Start() {
            leftHand = FindHand("Controller (left)", GloveLaterality.GLOVE_LEFT);
            rightHand = FindHand("Controller (right)", GloveLaterality.GLOVE_RIGHT);

            var gestureConnector = FindObjectOfType<GestureConnector>();

            leftGesture = gestureConnector.gestureSet["Nothing"];
            rightGesture = gestureConnector.gestureSet["Nothing"];


            gestureConnector.onLeftGesture += (Gesture gesture) => {
                if (leftGesture != gesture) {
                    Debug.Log("Left Hand: " + gesture.ToString());

                    leftHand[leftGesture].OnExit(this, leftHand);
                    leftGesture = gesture;
                    leftHand[leftGesture].OnEnter(this, leftHand);

                    if (leftHand.renderer != null) {
                        leftHand.renderer.material = gesture.material;
                    }
                }
            };
            gestureConnector.onRightGesture += (Gesture gesture) => {
                if (rightGesture != gesture) {
                    Debug.Log("Right Hand: " + gesture.ToString());

                    rightHand[rightGesture].OnExit(this, rightHand);
                    rightGesture = gesture;
                    rightHand[rightGesture].OnEnter(this, rightHand);

                    if (rightHand.renderer != null) {
                        rightHand.renderer.material = gesture.material;
                    }
                }
            };
        }

        void Update() {
            leftHand[leftGesture].OnUpdate(this, leftHand);
            rightHand[rightGesture].OnUpdate(this, rightHand);
        }

        private Hand FindHand(string label, GloveLaterality side) {
            var handTransform = FindObjectOfType<SteamVR_PlayArea>().transform.Find(label);
            Hand hand = new Hand {
                tracker = handTransform.gameObject,
                side = side
            };
            return hand;
        }
    }

}