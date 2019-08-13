using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.Spells.FireBall {
    public class Anchor : MonoBehaviour {
        private SpringJoint joint;

        private void Awake() {
            joint = GetComponent<SpringJoint>();
        }

        public void ConnectTo(Ball ball) {
            ball.explodable = false;
            ball.transform.position = transform.position;
            joint.connectedBody = ball.rigidbody;
        }

        public void ReleaseFrom(Ball ball) {
            ball.explodable = true;
            ball.transform.parent = ball.transform.parent.parent;
            joint.connectedBody = null;
        }

        private void Update() {
            if (joint.connectedBody) {
                var distance = transform.position - joint.connectedBody.transform.position;
                joint.connectedBody.AddForce(distance * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }
}