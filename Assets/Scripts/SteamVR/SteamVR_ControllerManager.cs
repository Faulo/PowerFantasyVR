using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVR_ControllerManager : MonoBehaviour {
    public GameObject left => SteamConnector.leftTracker.gameObject;
    public GameObject right => SteamConnector.rightTracker.gameObject;
}
