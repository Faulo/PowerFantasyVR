using PFVR.Player;
using PFVR.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneActivator : MonoBehaviour {
    public Gesture gesture;
    public GameObject stone;

    // Start is called before the first frame update
    void Start() {
        GestureConnector.onGestureUnlock += ActivateStone;
        GestureConnector.onGestureLock += DeactivateStone;
    }

    private void ActivateStone(Gesture gesture) {
        if (this.gesture.Equals(gesture)) {
            stone.SetActive(true);
            stone.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", gesture.spellColor);
        }
    }
    private void DeactivateStone(Gesture gesture) {
        if (this.gesture.Equals(gesture)) {
            stone.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
