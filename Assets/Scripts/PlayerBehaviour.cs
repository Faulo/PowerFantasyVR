using PFVR.Tracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR {

    public class PlayerBehaviour : MonoBehaviour {
        private Renderer leftControllerRenderer {
            get {
                return FindObjectOfType<SteamVR_ControllerManager>().left.transform.Find("Model").gameObject.GetComponentInChildren<Renderer>();
            }
        }
        private Renderer rightControllerRenderer {
            get {
                return FindObjectOfType<SteamVR_ControllerManager>().right.transform.Find("Model").gameObject.GetComponentInChildren<Renderer>();
            }
        }

        private Gesture leftGesture;
        private Gesture rightGesture;

        void Start() {
            var gestureConnector = FindObjectOfType<GestureConnector>();

            leftGesture = gestureConnector.gestureSet["Nothing"];
            rightGesture = gestureConnector.gestureSet["Nothing"];


            gestureConnector.onLeftGesture += (Gesture gesture) => {
                if (leftGesture != gesture) {
                    Debug.Log("Left Hand: " + gesture.ToString());

                    leftGesture.state.OnExit(this);
                    leftGesture = gesture;
                    leftGesture.state.OnEnter(this);

                    if (leftControllerRenderer != null) {
                        leftControllerRenderer.material = gesture.material;
                    }
                }
            };
            gestureConnector.onRightGesture += (Gesture gesture) => {
                if (rightGesture != gesture) {
                    Debug.Log("Right Hand: " + gesture.ToString());

                    rightGesture.state.OnExit(this);
                    rightGesture = gesture;
                    rightGesture.state.OnEnter(this);

                    if (rightControllerRenderer != null) {
                        rightControllerRenderer.material = gesture.material;
                    }
                }
            };
        }

        void Update() {
            leftGesture.state.OnUpdate(this);
            rightGesture.state.OnUpdate(this);
        }
    }

}