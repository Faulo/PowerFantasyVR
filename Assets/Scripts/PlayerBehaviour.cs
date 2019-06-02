using ManusVR.Core.Apollo;
using PFVR.Tracking;
using UnityEngine;

namespace PFVR {

    public class PlayerBehaviour : MonoBehaviour {
        public new Rigidbody rigidbody {
            get {
                return GetComponent<Rigidbody>();
            }
        }
        [SerializeField]
        private PlayerHandBehaviour leftHand = default;

        [SerializeField]
        private PlayerHandBehaviour rightHand = default;

        private Gesture leftGesture;
        private Gesture rightGesture;

        void Start() {
            leftHand.Init(this, GloveLaterality.GLOVE_LEFT);
            rightHand.Init(this, GloveLaterality.GLOVE_RIGHT);

            GestureConnector.onLeftGesture += leftHand.SetGesture;
            GestureConnector.onRightGesture += rightHand.SetGesture;
        }

        void Update() {
        }
    }

}