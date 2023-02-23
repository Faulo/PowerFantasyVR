using UnityEngine;

namespace PFVR.Player {
    public static class SteamConnector {
        public static Transform leftTracker {
            get {
                if (leftTrackerCache == null) {
                    try {
                        leftTrackerCache = Object.FindObjectOfType<SteamVR_ControllerManager>().left.transform;
                    } catch (System.Exception) {
                        Debug.Log("Scene is missing either 'SteamVR_ControllerManager' or its left field, help!");
                    }
                }
                return leftTrackerCache;
            }
        }
        static Transform leftTrackerCache;
        public static Transform rightTracker {
            get {
                if (rightTrackerCache == null) {
                    try {
                        rightTrackerCache = Object.FindObjectOfType<SteamVR_ControllerManager>().right.transform;
                    } catch (System.Exception) {
                        Debug.Log("Scene is missing either 'SteamVR_ControllerManager' or its right field!");
                    }
                }
                return rightTrackerCache;
            }
        }
        static Transform rightTrackerCache;
    }
}