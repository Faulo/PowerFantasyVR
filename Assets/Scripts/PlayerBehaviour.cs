using ManusVR.Core.Apollo;
using PFVR.ScriptableObjects;
using PFVR.Tracking;
using UnityEngine;

namespace PFVR {

    public class PlayerBehaviour : MonoBehaviour {
        [SerializeField]
        [Tooltip("Steam's Controller (left) object")]
        public Transform leftTracker;
        [SerializeField]
        [Tooltip("Manus' hand_l object")]
        public Transform leftWrist;

        [SerializeField]
        [Tooltip("Steam's Controller (right) object")]
        public Transform rightTracker;
        [SerializeField]
        [Tooltip("Manus' hand_r object")]
        public Transform rightWrist;

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