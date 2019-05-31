using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ML;
using PFVR.DataModels;
using System.IO;
using PFVR.Tracking;

public class GameBehaviour : MonoBehaviour {
    void Start()
    {
        var gestureConnector = FindObjectOfType<GestureConnector>();
        gestureConnector.onLeftGesture += (Gesture gesture) => {
            Debug.Log("Left Hand: " + gesture);
        };
        gestureConnector.onRightGesture += (Gesture gesture) => {
            Debug.Log("Right Hand: " + gesture);
        };
    }
}
