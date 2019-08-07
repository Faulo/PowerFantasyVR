using ManusVR.Core.Apollo;
using PFVR.ScriptableObjects;
using PFVR.Player;
using UnityEngine;
using System.Collections.Generic;
using PFVR.OurPhysics;

namespace PFVR.Player {
    public class PlayerBehaviour : MonoBehaviour {
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