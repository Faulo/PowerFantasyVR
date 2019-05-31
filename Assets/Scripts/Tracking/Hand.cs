using ManusVR.Core.Apollo;
using PFVR.DataModels;
using PFVR.Gestures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Tracking {
    public class Hand {
        public GameObject tracker;
        public Renderer renderer {
            get {
                return tracker.GetComponentInChildren<Renderer>();
            }
        }
        public GloveLaterality side;

        public IGestureState this[Gesture gesture] {
            get {
                if (!states.ContainsKey(gesture)) {
                    states[gesture] = UnityEngine.Object.Instantiate(gesture.statePrefab, tracker.transform).GetComponent<IGestureState>();
                }
                return states[gesture];
            }
        }
        private Dictionary<Gesture, IGestureState> states = new Dictionary<Gesture, IGestureState>();
    }
}
