using ManusVR.Core.Apollo;
using System;
using UnityEngine;

namespace PFVR.Tracking {
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