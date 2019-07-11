using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace PFVR.Player {
    public class GestureConnector : MonoBehaviour {
        [SerializeField]
        private GestureProfile gestureProfile = default;

        [SerializeField]
        private int gestureTriggerFrames = 1;

        private Dictionary<string, bool> unlockedGestures = new Dictionary<string, bool>() {
            ["Nothing"] = true
        };

        public IEnumerable<Gesture> availableGestures => unlockedGestures
            .Where(keyval => keyval.Value)
            .Select(keyval => gestureProfile.gestureSet[keyval.Key]);

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
                if (nextLeftGestureCount >= gestureTriggerFrames && IsUnlocked(gestureId)) {
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
                if (nextRightGestureCount >= gestureTriggerFrames && IsUnlocked(gestureId)) {
                    var gesture = gestureProfile.gestureSet[gestureId];
                    onRightGesture?.Invoke(gesture);
                }
            };
        }
        public bool IsUnlocked(string gestureId) {
            return unlockedGestures.ContainsKey(gestureId)
                ? unlockedGestures[gestureId]
                : false;
        }
        public bool IsUnlocked(Gesture gesture) => IsUnlocked(gesture.name);

        public void Unlock(string gestureId) {
            unlockedGestures[gestureId] = true;
        }
        public void Unlock(Gesture gesture) => Unlock(gesture.name);
        public void Lock(string gestureId) {
            unlockedGestures[gestureId] = false;
        }
        public void Lock(Gesture gesture) => Lock(gesture.name);

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                var id = "JetPack";
                if (IsUnlocked(id)) {
                    Lock(id);
                } else {
                    Unlock(id);
                }
            }
        }
    }
}
