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

        new public string ToString() {
            return id;
        }
    }
}