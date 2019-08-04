using PFVR.DataModels;
using PFVR.ScriptableObjects;
using Slothsoft.UnityExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace PFVR.Player {
    public class GestureConnector : MonoBehaviour {
        public static GestureConnector instance { get; private set; }

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

        public static event NewGesture onGestureUnlock;
        public static event NewGesture onGestureLock;

        private string nextLeftGestureId;
        private string nextRightGestureId;

        private int nextLeftGestureCount;
        private int nextRightGestureCount;

        private Gesture currentComplexGesture;

        [SerializeField]
        private KeyCode[] debugKeys = default;
        [SerializeField]
        private Gesture[] debugGestures = default;
        private Gesture defaultGesture => debugGestures[0];


        void Start() {
            if (gestureProfile == null) {
                throw new MissingReferenceException("GestureConnector needs a GestureProfile!");
            }

            instance = this;

            var recognizer = new GestureRecognizer(gestureProfile.modelDataPath);

            ManusConnector.onLeftGloveData += (GloveData glove) => {
                if (currentComplexGesture) {
                    onLeftGesture?.Invoke(currentComplexGesture);
                } else {
                    var gestureId = recognizer.Guess(glove.ToGestureModel());
                    gestureId = UnlockedOrDefault(gestureId);
                    if (nextLeftGestureId != gestureId) {
                        nextLeftGestureId = gestureId;
                        nextLeftGestureCount = 0;
                    }
                    nextLeftGestureCount++;
                    if (nextLeftGestureCount >= gestureTriggerFrames) {
                        var gesture = gestureProfile.gestureSet[gestureId];
                        onLeftGesture?.Invoke(gesture);
                    }
                }
            };
            ManusConnector.onRightGloveData += (GloveData glove) => {
                if (currentComplexGesture) {
                    onRightGesture?.Invoke(currentComplexGesture);
                } else {
                    var gestureId = recognizer.Guess(glove.ToGestureModel());
                    gestureId = UnlockedOrDefault(gestureId);
                    if (nextRightGestureId != gestureId) {
                        nextRightGestureId = gestureId;
                        nextRightGestureCount = 0;
                    }
                    nextRightGestureCount++;
                    if (nextRightGestureCount >= gestureTriggerFrames) {
                        var gesture = gestureProfile.gestureSet[gestureId];
                        onRightGesture?.Invoke(gesture);
                    }
                }
            };
            StartCoroutine(Init());
        }

        public bool CanStartComplexGesture(Gesture gesture) {
            return currentComplexGesture != gesture && gesture.isComplex && gesture == UnlockedOrDefault(gesture);
        }
        public void StartComplexGesture(Gesture gesture) {
            currentComplexGesture = gesture;
        }
        public void StopComplexGesture(Gesture gesture) {
            if (currentComplexGesture == gesture) {
                currentComplexGesture = null;
            }
        }

        private string UnlockedOrDefault(string gestureId) {
            return IsUnlocked(gestureId)
                ? gestureId
                : defaultGesture.name;
        }
        private Gesture UnlockedOrDefault(Gesture gesture) {
            return IsUnlocked(gesture)
                ? gesture
                : defaultGesture;
        }

        private IEnumerator Init() {
            yield return new WaitForSeconds(1);
            unlockedGestures.Keys.ToArray().ForAll(Unlock);
            onLeftGesture?.Invoke(defaultGesture);
            onRightGesture?.Invoke(defaultGesture);
            yield return null;
        }
        public bool IsUnlocked(string gestureId) {
            return unlockedGestures.ContainsKey(gestureId)
                ? unlockedGestures[gestureId]
                : false;
        }
        public bool IsUnlocked(Gesture gesture) => IsUnlocked(gesture.name);

        public void Unlock(string gestureId) {
            unlockedGestures[gestureId] = true;
            onGestureUnlock?.Invoke(gestureProfile.gestureSet[gestureId]);
        }
        public void Unlock(Gesture gesture) => Unlock(gesture.name);
        public void Lock(string gestureId) {
            unlockedGestures[gestureId] = false;
            onGestureLock?.Invoke(gestureProfile.gestureSet[gestureId]);
        }
        public void Lock(Gesture gesture) => Lock(gesture.name);

        private void Update() {
            for (int i = 0; i < debugKeys.Length; i++) {
                if (Input.GetKeyDown(debugKeys[i])) {
                    var id = debugGestures[i];
                    if (IsUnlocked(id)) {
                        Lock(id);
                    } else {
                        Unlock(id);
                    }
                }
            }
        }
    }
}
