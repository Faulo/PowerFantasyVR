using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PFVR.DataModels;
using PFVR.ScriptableObjects;
using Slothsoft.UnityExtensions;
using UnityEngine;


namespace PFVR.Player {
    public sealed class GestureConnector : MonoBehaviour, IGestureDictionary {
        public static GestureConnector instance { get; private set; }

        [SerializeField]
        GestureProfile gestureProfile = default;

        [SerializeField]
        int gestureTriggerFrames = 1;

        [SerializeField, HideInInspector]
        Gesture[] unlockedGestures = new Gesture[0];

        public IEnumerable<Gesture> possibleGestures => gestureProfile.gestureSet.gestureObjects;
        public IEnumerable<Gesture> availableGestures => unlockedGestures;

        public delegate void NewGesture(Gesture gesture);

        public static event NewGesture onLeftGesture;
        public static event NewGesture onRightGesture;

        public static event NewGesture onGestureUnlock;
        public static event NewGesture onGestureLock;

        string nextLeftGestureId;
        string nextRightGestureId;

        int nextLeftGestureCount;
        int nextRightGestureCount;

        Gesture currentComplexGesture;

        [SerializeField]
        KeyCode[] debugKeys = default;
        [SerializeField]
        Gesture[] debugGestures = default;
        Gesture defaultGesture => debugGestures[0];

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
                    string gestureId = recognizer.Guess(glove.ToGestureModel());
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
                    string gestureId = recognizer.Guess(glove.ToGestureModel());
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

        string UnlockedOrDefault(string gestureId) {
            return IsUnlocked(gestureId)
                ? gestureId
                : defaultGesture.name;
        }
        Gesture UnlockedOrDefault(Gesture gesture) {
            return IsUnlocked(gesture)
                ? gesture
                : defaultGesture;
        }

        IEnumerator Init() {
            yield return new WaitForSeconds(1);
            unlockedGestures.ForAll(Unlock);
            onLeftGesture?.Invoke(defaultGesture);
            onRightGesture?.Invoke(defaultGesture);
            yield return null;
        }

        public bool IsUnlocked(string gestureId) {
            return unlockedGestures.Any(gesture => gesture.name == gestureId);
        }
        public bool IsUnlocked(Gesture gesture) => unlockedGestures.Contains(gesture);

        public void Unlock(string gestureId) => Unlock(gestureProfile.gestureSet[gestureId]);
        public void Unlock(Gesture gesture) {
            AddGesture(gesture);
            onGestureUnlock?.Invoke(gesture);
        }

        public void Lock(string gestureId) => Lock(gestureProfile.gestureSet[gestureId]);
        public void Lock(Gesture gesture) {
            RemoveGesture(gesture);
            onGestureLock?.Invoke(gesture);
        }

        void Update() {
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

        public bool HasGesture(Gesture gesture) => unlockedGestures.Contains(gesture);
        public void AddGesture(Gesture gesture) {
            if (!HasGesture(gesture)) {
                var list = unlockedGestures.ToList();
                list.Add(gesture);
                unlockedGestures = list.ToArray();
            }
        }
        public void RemoveGesture(Gesture gesture) {
            if (HasGesture(gesture)) {
                var list = unlockedGestures.ToList();
                list.Remove(gesture);
                unlockedGestures = list.ToArray();
            }
        }
    }
}
