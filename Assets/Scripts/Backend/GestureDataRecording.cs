using PFVR.Canvas;
using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Backend {
    public class GestureDataRecording : MonoBehaviour {
        [SerializeField]
        private LayoutGroup gestureSetGroup = default;
        [SerializeField]
        private LayoutGroup gestureGroup = default;
        [SerializeField]
        private TextMeshProUGUI log = default;

        [SerializeField]
        [Range(1,60)]
        private int recordingTime = 10;

        private ScriptableObjectManager<GestureSet> gestureSetManager;
        private ScriptableObjectManager<Gesture> gestureManager;

        private GestureSet currentGestureSet;
        private Gesture currentGesture;
        private Coroutine currentRoutine;
        private GestureRecorder currentRecorder;

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
            if (currentRoutine != null) {
                log.text = "Wait for the last recording to finish!";
                return;
            }
            var gestures = gestureManager.elements
                .Where(gesture => currentGestureSet.gestureNames.Contains(gesture.name));
            currentRoutine = StartCoroutine(RecordGesturesRoutine(gestures.ToArray()));
        }
        public void RecordCurrentGesture() {
            if (currentGesture == null) {
                log.text = "Select a gesture first!";
                return;
            }
            if (currentRoutine != null) {
                log.text = "Wait for the last recording to finish!";
                return;
            }
            currentRoutine = StartCoroutine(RecordGesturesRoutine(currentGesture));
        }
        private IEnumerator RecordGesturesRoutine(params Gesture[] gestures) {
            foreach (var gesture in gestures) {
                currentRecorder = new GestureRecorder(gesture, recordingTime, log);
                yield return currentRecorder.Record();
            }
            currentRoutine = null;
        }
        public void CreateGestureSetCSV() {
            if (currentGestureSet == null) {
                log.text = "Select a gesture set first!";
                return;
            }
            if (currentRoutine != null) {
                log.text = "Wait for the last recording to finish!";
                return;
            }
            var merger = new ModelMerger(currentGestureSet.gestureNames.Select(name => "TrackingData/" + name));
            var fileName = currentGestureSet.name + "." + DateTime.Now.ToFileTime() + ".csv";
            merger.Put("Assets/Resources/TrackingData/" + fileName);
            log.text = "Created gesture set model '" + fileName + "'!";
        }
        private void OnApplicationQuit() {
            if (currentRecorder != null) {
                currentRecorder.Abort();
                StopCoroutine(currentRoutine);
            }
        }
    }
}