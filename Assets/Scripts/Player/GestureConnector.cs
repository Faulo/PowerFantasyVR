using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.Player {
    public class GestureConnector : MonoBehaviour {
        [SerializeField]
        private GestureSet gestureSet = default;

        [SerializeField]
        private int gestureTriggerFrames = 1;

        public IEnumerable<Gesture> availableGestures => gestureSet.gestureObjects;

        public delegate void NewGesture(Gesture gesture);

        public static event NewGesture onLeftGesture;
        public static event NewGesture onRightGesture;

        private string nextLeftGestureId;
        private string nextRightGestureId;

        private int nextLeftGestureCount;
        private int nextRightGestureCount;

        void Start() {
            if (gestureSet == null) {
                throw new MissingReferenceException("GestureConnector needs a gestureSet!");
            }

            var recognizer = new GestureRecognizer(gestureSet.modelPath);

            ManusConnector.onLeftGloveData += (GloveData glove) => {
                var gestureId = recognizer.Guess(glove.ToGestureModel());
                if (nextLeftGestureId != gestureId) {
                    nextLeftGestureId = gestureId;
                    nextLeftGestureCount = 0;
                }
                nextLeftGestureCount++;
                if (nextLeftGestureCount >= gestureTriggerFrames) {
                    var gesture = gestureSet[gestureId];
                    onLeftGesture?.Invoke(gesture);
                }
            };
            ManusConnector.onRightGloveData += (GloveData glove) => {
                var gestureId = recognizer.Guess(glove.ToGestureModel());
                if (nextRightGestureId != gestureId) {
                    nextRightGestureId = gestureId;
                    nextRightGestureCount = 0;
                }
                nextRightGestureCount++;
                if (nextRightGestureCount >= gestureTriggerFrames) {
                    var gesture = gestureSet[gestureId];
                    onRightGesture?.Invoke(gesture);
                }
            };
        }
    }
}
