using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PFVR.Tracking {
    public class GestureRecorder : MonoBehaviour {
        enum RecordState {
            Inactive,
            Preparing,
            Recording,
            Done
        }
        [SerializeField]
        private GestureSet gesturesToRecord = default;
        [SerializeField]
        private int recordingTime = 10;

        private ModelWriter<GestureModel> writer;
        private string currentGesture;
        private RecordState state = RecordState.Inactive;

        // Start is called before the first frame update
        void Start() {
            writer = new ModelWriter<GestureModel>(Application.dataPath + "/Resources/Tracking");

            ManusConnector.onLeftGloveData += RecordGlove;
            ManusConnector.onRightGloveData += RecordGlove;

            StartCoroutine(Record());
        }

        private IEnumerator Record() {
            foreach (var gesture in gesturesToRecord.gestureNames) {
                state = RecordState.Preparing;
                currentGesture = gesture;
                Log("Recording '" + gesture + "' in 3...");
                yield return new WaitForSeconds(1);
                Log("Recording '" + gesture + "' in 2...");
                yield return new WaitForSeconds(1);
                Log("Recording '" + gesture + "' in 1...");
                yield return new WaitForSeconds(1);
                Log("Recording '" + gesture + "' NOW!");
                state = RecordState.Recording;
                yield return new WaitForSeconds(recordingTime);
                Log("Done recording '" + gesture + "'!");
                yield return new WaitForSeconds(1);
            }
            state = RecordState.Done;
            Log("All done!");
            writer.Finish();
            Application.Quit();
        }
        private void RecordGlove(GloveData data) {
            switch (state) {
                case RecordState.Inactive:
                    break;
                case RecordState.Preparing:
                    break;
                case RecordState.Recording:
                    var model = data.ToGestureModel();
                    model.gesture = currentGesture;
                    writer.Append(model);
                    break;
                case RecordState.Done:
                    break;
            }
        }
        private void Log(string text) {
            Debug.Log(text);
            GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
    }
}
