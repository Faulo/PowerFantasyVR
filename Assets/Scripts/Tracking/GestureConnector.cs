using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.Tracking {
    public class GestureConnector : MonoBehaviour {
        [SerializeField]
        public GestureSet gestureSet;

        public Gesture leftHand { get; private set; }
        public Gesture rightHand { get; private set; }

        public delegate void NewGesture(Gesture gesture);

        public event NewGesture onLeftGesture;
        public event NewGesture onRightGesture;

        void Start() {
            if (gestureSet == null) {
                throw new MissingReferenceException("GestureConnector needs a gestureSet!");
            }
            var manus = FindObjectOfType<ManusConnector>();
            if (manus == null) {
                throw new MissingComponentException("GestureConnector needs a ManusConnector somewhere!");
            }
            var recognizer = new GestureRecognizer();

            manus.onLeftHandData += (OneHand hand) => {
                var gesture = recognizer.Guess(hand);
                leftHand = gestureSet[gesture];
                onLeftGesture(leftHand);
            };
            manus.onRightHandData += (OneHand hand) => {
                var gesture = recognizer.Guess(hand);
                rightHand = gestureSet[gesture];
                onRightGesture(rightHand);
            };
        }
    }
}
