using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.Player {
    public class GestureConnector : MonoBehaviour {
        [SerializeField]
        private GestureProfile gestureProfile = default;

        [SerializeField]
        private int gestureTriggerFrames = 1;

        public IEnumerable<Gesture> availableGestures => gestureProfile.gestureSet.gestureObjects;

        public delegate void NewGesture(Gesture gesture);

        public static event NewGesture onLeftGesture;
        public static event NewGesture onRightGesture;

        private string nextLeftGestureId;
        private string nextRightGestureId;

        private int nextLeftGestureCount;
        private int nextRightGestureCount;

        void Start() {
            if (gestureProfile == null) {
                throw new MissingReferenceException("GestureConnector needs a GestureProfile!");
            }

            var recognizer = new GestureRecognizer(gestureProfile.modelDataPath);

            ManusConnector.onLeftGloveData += (GloveData glove) => {
                var gestureId = recognizer.Guess(glove.ToGestureModel());
                if (nextLeftGestureId != gestureId) {
                    nextLeftGestureId = gestureId;
                    nextLeftGestureCount = 0;
                }
                nextLeftGestureCount++;
                if (nextLeftGestureCount >= gestureTriggerFrames) {
                    var gesture = gestureProfile.gestureSet[gestureId];
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
                    var gesture = gestureProfile.gestureSet[gestureId];
                    onRightGesture?.Invoke(gesture);
                }
            };
        }
    }
}
