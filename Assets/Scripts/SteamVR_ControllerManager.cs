using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVR_ControllerManager : MonoBehaviour {
    public GameObject left {
        get {
            return FindObjectOfType<SteamVR_PlayArea>().transform.Find("Controller (left)").gameObject;
        }
    }
    public GameObject right {
        get {
            return FindObjectOfType<SteamVR_PlayArea>().transform.Find("Controller (right)").gameObject;
        }
    }
}
