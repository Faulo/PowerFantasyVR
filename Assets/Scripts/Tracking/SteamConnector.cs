using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace PFVR.Tracking {
    public class SteamConnector : MonoBehaviour {
        public GameObject leftHand {
            get {
                if (leftHandCache == null) {
                    leftHandCache = FindObjectOfType<SteamVR_PlayArea>().transform.Find("Controller (left)").gameObject;
                }
                return leftHandCache;
            }
        }
        private GameObject leftHandCache;
        public GameObject rightHand {
            get {
                if (rightHandCache == null) {
                    rightHandCache = FindObjectOfType<SteamVR_PlayArea>().transform.Find("Controller (right)").gameObject;
                }
                return rightHandCache;
            }
        }
        private GameObject rightHandCache;

        public delegate void NewTrackerData(GameObject tracker);

        public event NewTrackerData onLeftTrackerData;
        public event NewTrackerData onRightTrackerData;

        void Update() {
            if (leftHand != null) {
                onLeftTrackerData?.Invoke(leftHandCache);
            }
            if (rightHand != null) {
                onRightTrackerData?.Invoke(rightHandCache);
            }
        }
    }
}