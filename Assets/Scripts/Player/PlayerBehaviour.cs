using ManusVR.Core.Apollo;
using PFVR.ScriptableObjects;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Player {
    public class PlayerBehaviour : MonoBehaviour {
        public new Rigidbody rigidbody => GetComponent<Rigidbody>();
        [SerializeField]
        private PlayerHandBehaviour leftHand = default;

        [SerializeField]
        private PlayerHandBehaviour rightHand = default;
        
        public Transform torso;

        private Gesture leftGesture;
        private Gesture rightGesture;

        public Vector3 deltaMovement { get; private set; }
        private Vector3 lastPosition = default;

        public float speed => rigidbody.velocity.magnitude;
        public Vector3 velocity => rigidbody.velocity;

        void Start() {
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