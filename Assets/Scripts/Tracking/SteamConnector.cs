using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace PFVR.DataModels {
    public class SteamConnector {
        public static Transform leftTracker {
            get {
                if (leftTrackerCache == null) {
                    try {
                        leftTrackerCache = Object.FindObjectOfType<SteamVR_PlayArea>().transform.Find("Controller (left)");
                    } catch(System.Exception e) {
                        Debug.Log("Scene is missing either 'SteamVR_PlayArea' or 'Controller (left)', help!");
                    }
                }
                return leftTrackerCache;
            }
        }
        private static Transform leftTrackerCache;
        public static Transform rightTracker {
            get {
                if (rightTrackerCache == null) {
                    try {
                        rightTrackerCache = Object.FindObjectOfType<SteamVR_PlayArea>().transform.Find("Controller (right)");
                    } catch (System.Exception e) {
                        Debug.Log("Scene is missing either 'SteamVR_PlayArea' or 'Controller (right)', help!");
                    }
                }
                return rightTrackerCache;
            }
        }
        private static Transform rightTrackerCache;
    }
}