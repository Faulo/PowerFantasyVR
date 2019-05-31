using PFVR.Gestures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Tracking {
    [Serializable]
    public class Gesture {
        public string id;
        public Material material;
        public GameObject statePrefab;

        public IGestureState state {
            get {
                Debug.Log(statePrefab.GetComponent<IGestureState>());
                return statePrefab.GetComponent<IGestureState>();
            }
        }

        new public string ToString() {
            return id;
        }
    }
}