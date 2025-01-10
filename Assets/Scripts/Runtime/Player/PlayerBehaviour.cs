using ManusVR.Core.Apollo;
using PFVR.OurPhysics;
using UnityEngine;

namespace PFVR.Player {
    public sealed class PlayerBehaviour : MonoBehaviour {
        public static PlayerBehaviour instance {
            get {
                if (instanceCache == null) {
                    instanceCache = FindObjectOfType<PlayerBehaviour>();
                }
                return instanceCache;
            }
        }
        static PlayerBehaviour instanceCache;

        public IMotor motor { get; private set; }

        [SerializeField]
        public PlayerHandBehaviour leftHand = default;

        [SerializeField]
        public PlayerHandBehaviour rightHand = default;

        void Start() {
            motor = GetComponent<IMotor>();

            if (leftHand) {
                leftHand.Init(this, GloveLaterality.GLOVE_LEFT);
                GestureConnector.onLeftGesture += leftHand.SetGesture;
            }
            if (rightHand) {
                rightHand.Init(this, GloveLaterality.GLOVE_RIGHT);
                GestureConnector.onRightGesture += rightHand.SetGesture;
            }
        }
    }
}