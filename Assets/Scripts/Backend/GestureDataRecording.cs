using PFVR.Canvas;
using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Backend {
    public class GestureDataRecording : MonoBehaviour {
        [SerializeField, Range(1, 60)]
        private int recordingTime = 10;
        [SerializeField, Range(1, 600)]
        private int trainingTime = 60;
        [Space]
        [SerializeField]
        private LayoutGroup gestureSetGroup = default;
        [SerializeField]
        private LayoutGroup gestureGroup = default;
        [SerializeField]
        private TextMeshProUGUI log = default;

        private ScriptableObjectManager<GestureProfile> gestureProfileManager;
        private ScriptableObjectManager<Gesture> gestureManager;

        private GestureProfile currentProfile;
        private GestureSet currentGestureSet;
        private Gesture currentGesture;
        private Coroutine currentRoutine;
        private GestureRecorder currentRecorder;

        // Start is called before the first frame update
        void Start() {
            gestureProfileManager = new ScriptableObjectManager<GestureProfile>(gestureSetGroup);
            gestureManager = new ScriptableObjectManager<Gesture>(gestureGroup);

            gestureProfileManager.AddClickAction((profile, button) => {
                currentProfile = profile;
                button.selected = true;
                currentGestureSet = profile.gestureSet;
                gestureManager.OnlyShow((gesture) => {
                    return !gesture.isComplex && currentGestureSet.gestureNames.Contains(gesture.name);
                });
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
                currentRecorder = new GestureRecorder(currentProfile, gesture, recordingTime, log);
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
            var merger = new ModelMerger(currentGestureSet.gestureNames.Select(name => "TrackingData/" + currentProfile.name + "/" + name));
            merger.Put(currentProfile.trackingDataPath);
            log.text = "Created gesture set model '" + Path.GetFileName(currentProfile.trackingDataPath) + "'!";
        }
        public void CompileGestureSetZIP() {
            if (currentGestureSet == null) {
                log.text = "Select a gesture set first!";
                return;
            }
            if (currentRoutine != null) {
                log.text = "Wait for the last recording to finish!";
                return;
            }
            try {
                var batchFile = Application.dataPath + "/../trainModel.bat";
                var name = currentProfile.name;
                var csvFile = currentProfile.trackingDataPath;
                var zipFile = currentProfile.modelDataPath;

                var args = string.Join(" ", new[] { name, csvFile, zipFile, trainingTime.ToString() }.Select(QuoteShellArgument));

                log.text = "Training model '" + Path.GetFileName(zipFile) + "' with data '" + Path.GetFileName(csvFile) + "'...";

                print(batchFile + " " + args);
                Process.Start(batchFile, args);
            } catch (Exception e) {
                print(e);
            }
        }
        private string QuoteShellArgument(string argument) {
            //TODO: find the proper C# way to do this 
            return "\"" + argument + "\"";
        }
        private void OnApplicationQuit() {
            if (currentRecorder != null) {
                currentRecorder.Abort();
                StopCoroutine(currentRoutine);
            }
        }
    }
}