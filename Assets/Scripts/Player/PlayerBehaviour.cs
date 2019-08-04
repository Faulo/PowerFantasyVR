using ManusVR.Core.Apollo;
using PFVR.ScriptableObjects;
using PFVR.Player;
using UnityEngine;
using System.Collections.Generic;
using PFVR.OurPhysics;

namespace PFVR.Player {
    [RequireComponent(typeof(GestureConnector))]
    public class PlayerBehaviour : MonoBehaviour {
        public IMotor motor { get; private set; }

        [SerializeField]
        public PlayerHandBehaviour leftHand = default;

        [SerializeField]
        public PlayerHandBehaviour rightHand = default;

        public Transform torso;

        private Gesture leftGesture;
        private Gesture rightGesture;

        public Vector3 deltaMovement { get; private set; }
        private Vector3 lastPosition = default;

        public IEnumerable<Gesture> availableGestures => GetComponent<GestureConnector>().availableGestures;

        void Start() {
            motor = GetComponent<IMotor>();

            leftHand.Init(this, GloveLaterality.GLOVE_LEFT);
            rightHand.Init(this, GloveLaterality.GLOVE_RIGHT);

            GestureConnector.onLeftGesture += leftHand.SetGesture;
            GestureConnector.onRightGesture += rightHand.SetGesture;
        }

        void Update() {
            deltaMovement = transform.position - lastPosition;
            lastPosition = transform.position;
        }
    }

}