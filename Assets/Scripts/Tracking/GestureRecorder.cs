using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Tracking {
    public class GestureRecorder : MonoBehaviour {
        private ManusConnector manus {
            get {
                return FindObjectOfType<ManusConnector>();
            }
        }

        [SerializeField]
        private string gestureToRecord;

        private float timer;
        private ModelWriter<OneHand> writer;

        // Start is called before the first frame update
        void Start() {
            writer = new ModelWriter<OneHand>(Application.dataPath + "/Resources/Tracking");
        }

        // Update is called once per frame
        void Update() {
            timer += Time.deltaTime;
            if (timer > 10) {
                Debug.Log("DONE!");
                return;
            }
            if (timer > 3) {
                Debug.Log("RECORDING!");
                var model = manus.leftHand;
                model.gesture = gestureToRecord;
                writer.Append(model);
                return;
            }
            if (timer > 2) {
                Debug.Log("1...");
                return;
            }
            if (timer > 1) {
                Debug.Log("2...");
                return;
            }
            if (timer > 0) {
                Debug.Log("3...");
                return;
            }
        }
    }
}
