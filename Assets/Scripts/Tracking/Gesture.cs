using ManusVR.Core.Apollo;
using PFVR.Gestures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.DataModels {
    [Serializable]
    public class Gesture {
        public string id;
        public Material material;
        public GameObject statePrefab;

        [HideInInspector]
        public GloveLaterality laterality;
        [HideInInspector]
        public Transform tracker;

        new public string ToString() {
            return id;
        }
    }
}