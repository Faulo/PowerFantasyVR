using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PFVR.Canvas;
using PFVR.DataModels;
using PFVR.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Backend {
    /// <summary>
    /// The data model used by <see cref="GestureData"/>.
    /// </summary>
    public class GestureDataRecording : MonoBehaviour {
        [SerializeField, Range(1, 60)]
        int recordingTime = 10;
        [SerializeField, Range(1, 600)]
        int trainingTime = 60;
        [Space]
        [SerializeField]
        LayoutGroup gestureSetGroup = default;
        [SerializeField]
        LayoutGroup gestureGroup = default;
        [SerializeField]
        TextMeshProUGUI log = default;

        ScriptableObjectManager<GestureProfile> gestureProfileManager;
        ScriptableObjectManager<Gesture> gestureManager;

        GestureProfile currentProfile;
        GestureSet currentGestureSet;
        Gesture currentGesture;
        Coroutine currentRoutine;
        GestureRecorder currentRecorder;

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
                .Where(gesture => !gesture.isComplex && currentGestureSet.gestureNames.Contains(gesture.name));
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
        IEnumerator RecordGesturesRoutine(params Gesture[] gestures) {
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
                string batchFile = Application.dataPath + "/../trainModel.bat";
                string name = currentProfile.name;
                string csvFile = currentProfile.trackingDataPath;
                string zipFile = currentProfile.modelDataPath;

                string args = string.Join(" ", new[] { name, csvFile, zipFile, trainingTime.ToString() }.Select(QuoteShellArgument));

                log.text = "Training model '" + Path.GetFileName(zipFile) + "' with data '" + Path.GetFileName(csvFile) + "'...";

                print(batchFile + " " + args);
                Process.Start(batchFile, args);
            } catch (Exception e) {
                print(e);
            }
        }
        string QuoteShellArgument(string argument) {
            //TODO: find the proper C# way to do this 
            return "\"" + argument + "\"";
        }
        void OnApplicationQuit() {
            if (currentRecorder != null) {
                currentRecorder.Abort();
                StopCoroutine(currentRoutine);
            }
        }
    }
}