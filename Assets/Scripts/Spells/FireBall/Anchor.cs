using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    [RequireComponent(typeof(SpringJoint))]
    public class Anchor : MonoBehaviour {
        private SpringJoint joint => GetComponent<SpringJoint>();
        void Update() {
            if (joint.connectedBody != null) {
                /*
                var distance = transform.position - joint.connectedBody.transform.position;
                joint.connectedBody.AddForce(distance, ForceMode.VelocityChange);
                //anchor.damper = Mathf.Clamp(1 / distance.magnitude, 1, 1000);
                joint.spring = Mathf.Clamp(distance.magnitude, 1, 1000);
                //*/
            }
        }
    }
}
