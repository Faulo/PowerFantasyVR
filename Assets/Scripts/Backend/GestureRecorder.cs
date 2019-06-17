using PFVR.DataModels;
using PFVR.ScriptableObjects;
using PFVR.Player;
using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace PFVR.Backend {
    public class GestureRecorder {
        enum RecordState {
            Inactive,
            Preparing,
            Recording,
            Done
        }
        private TextMeshProUGUI log;
        private Gesture gesture;
        private int recordingTime;
        private ModelWriter<GestureModel> writer;
        private RecordState state = RecordState.Inactive;

        public GestureRecorder(Gesture gesture, int recordingTime, TextMeshProUGUI log) {
            this.gesture = gesture;
            this.log = log;
            this.recordingTime = recordingTime;
        }

        public IEnumerator Record() {
            state = RecordState.Preparing;

            string path = Application.dataPath + "/Resources/TrackingData/";
            if (!AssetDatabase.IsValidFolder("Assets/Resources/TrackingData/" + gesture.name)) {
                AssetDatabase.CreateFolder("Assets/Resources/TrackingData", gesture.name);
            }
            writer = new ModelWriter<GestureModel>(path + gesture.name);

            ManusConnector.onLeftGloveData += RecordGlove;
            ManusConnector.onRightGloveData += RecordGlove;
            
            Log("Recording '" + gesture.name + "' in 3...");
            yield return new WaitForSeconds(1);
            Log("Recording '" + gesture.name + "' in 2...");
            yield return new WaitForSeconds(1);
            Log("Recording '" + gesture.name + "' in 1...");
            yield return new WaitForSeconds(1);
            Log("Recording '" + gesture.name + "' NOW!");
            state = RecordState.Recording;
            yield return new WaitForSeconds(recordingTime);
            Log("Done recording '" + gesture.name + "'!");
            yield return new WaitForSeconds(1);
            state = RecordState.Done;
            Log("All done!");
            writer.Finish();
        }
        private void RecordGlove(GloveData data) {
            switch (state) {
                case RecordState.Inactive:
                    break;
                case RecordState.Preparing:
                    break;
                case RecordState.Recording:
                    var model = data.ToGestureModel();
                    model.gesture = gesture.name;
                    writer.Append(model);
                    break;
                case RecordState.Done:
                    break;
            }
        }
        private void Log(string text) {
            Debug.Log(text);
            if (log != default) {
                log.text = text;
            }
        }

        public void Abort() {
            writer.Finish();
            writer = null;
        }
    }
}
