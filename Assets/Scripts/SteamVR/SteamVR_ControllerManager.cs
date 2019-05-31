using PFVR.Tracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVR_ControllerManager : MonoBehaviour {
    public GameObject left {
        get {
            return FindObjectOfType<SteamConnector>().leftHand;
        }
    }
    public GameObject right {
        get {
            return FindObjectOfType<SteamConnector>().rightHand;
        }
    }
}
