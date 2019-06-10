using PFVR.OurPhysics;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ScalableObject))]
    public class Ball : MonoBehaviour {
        public float size {
            get => scale.scaling;
            set => scale.scaling = value;
        }

        private ScalableObject scale {
            get => GetComponent<ScalableObject>();
        }
        private Rigidbody body {
            get => GetComponent<Rigidbody>();
        }
        public void ConnectTo(Joint anchor) {
            transform.position = anchor.transform.position;
            anchor.connectedBody = body;
        }
        public void ReleaseFrom(Joint anchor, Vector3 launchVelocity) {
            anchor.connectedBody = null;
            body.velocity = launchVelocity + 2 * body.velocity;
            body.gameObject.AddComponent<KinematicRigidbody>();
        }
    }
}