using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.DataModels {
    public class GestureConnector : MonoBehaviour {
        [SerializeField]
        private GestureSet gestureSet;

        public delegate void NewGesture(Gesture gesture);

        public static event NewGesture onLeftGesture;
        public static event NewGesture onRightGesture;

        void Start() {
            if (gestureSet == null) {
                throw new MissingReferenceException("GestureConnector needs a gestureSet!");
            }

            var recognizer = new GestureRecognizer();

            ManusConnector.onLeftGloveData += (GloveData glove) => {
                var gestureId = recognizer.Guess(glove.ToGestureModel());
                var gesture = gestureSet[gestureId];
                gesture.tracker = glove.tracker;
                gesture.laterality = glove.laterality;
                onLeftGesture?.Invoke(gesture);
            };
            ManusConnector.onRightGloveData += (GloveData glove) => {
                var gestureId = recognizer.Guess(glove.ToGestureModel());
                var gesture = gestureSet[gestureId];
                gesture.tracker = glove.tracker;
                gesture.laterality = glove.laterality;
                onRightGesture?.Invoke(gesture);
            };
        }
    }
}
