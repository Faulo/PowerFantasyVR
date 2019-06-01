using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PFVR.Tracking {
    public class GestureRecorder : MonoBehaviour {
        enum RecordState {
            Inactive,
            Preparing,
            Recording,
            Done
        }
        [SerializeField]
        private string[] gesturesToRecord;

        private ModelWriter<OneHand> writer;
        private string currentGesture;
        private RecordState state = RecordState.Inactive;

        // Start is called before the first frame update
        void Start() {
            writer = new ModelWriter<OneHand>(Application.dataPath + "/Resources/Tracking");

            var manus = FindObjectOfType<ManusConnector>();
            manus.onLeftHandData += RecordHand;
            manus.onRightHandData += RecordHand;

            StartCoroutine(Record());
        }

        private IEnumerator Record() {
            foreach (var gesture in gesturesToRecord) {
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
                yield return new WaitForSeconds(10);
                Log("Done recording '" + gesture + "'!");
                yield return new WaitForSeconds(1);
            }
            state = RecordState.Done;
            Log("All done!");
            Application.Quit();
        }
        private void RecordHand(OneHand model) {
            switch (state) {
                case RecordState.Inactive:
                    break;
                case RecordState.Preparing:
                    break;
                case RecordState.Recording:
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
