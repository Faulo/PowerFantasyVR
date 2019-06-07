using PFVR.Canvas;
using PFVR.ScriptableObjects;
using PFVR.Tracking;
using Slothsoft.UnityExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GestureDataRecording : MonoBehaviour {
    [SerializeField]
    private LayoutGroup gestureSetGroup = default;
    [SerializeField]
    private LayoutGroup gestureGroup = default;
    [SerializeField]
    private TextMeshProUGUI log = default;

    private ScriptableObjectManager<GestureSet> gestureSetManager;
    private ScriptableObjectManager<Gesture> gestureManager;

    private GestureSet currentGestureSet;
    private Gesture currentGesture;

    // Start is called before the first frame update
    void Start() {
        gestureSetManager = new ScriptableObjectManager<GestureSet>(gestureSetGroup);
        gestureManager = new ScriptableObjectManager<Gesture>(gestureGroup);

        gestureSetManager.AddClickAction((set, button) => {
            gestureManager.OnlyShow((gesture) => {
                return set.gestureNames.Contains(gesture.name);
            });
            currentGestureSet = set;
            button.selected = true;
        });

        gestureManager.AddClickAction((gesture, button) => {
            currentGesture = gesture;
            button.selected = true;
        });
    }

    public void RecordCurrentGestureSet() {
        if (currentGestureSet == null) {
            log.text = "Select a gesture set first!";
            return;
        }
    }
    public void RecordCurrentGesture() {
        if (currentGesture == null) {
            log.text = "Select a gesture first!";
            return;
        }
        var recorder = gameObject.AddComponent<GestureRecorder>();
        recorder.gestureToRecord = currentGesture;
        recorder.log = log;
    }
    public void CreateGestureSetCSV() {
        if (currentGestureSet == null) {
            log.text = "Select a gesture set first!";
            return;
        }
    }
}
