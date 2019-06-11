using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ScalableObject))]
    public class Ball : MonoBehaviour {
        [SerializeField, Range(1, 10)]
        private float velocityMultiplier = 1;

        public float size {
            get => scale.scaling;
            set => scale.scaling = value;
        }

        private ScalableObject scale => GetComponent<ScalableObject>();
        private Rigidbody body => GetComponent<Rigidbody>();
        private new Collider collider => GetComponent<Collider>();
        private new Renderer renderer => GetComponent<Renderer>();

        public event Action<Ball, Collision> onCollisionEnter;

        public void ConnectTo(Joint anchor) {
            collider.enabled = false;
            transform.position = anchor.transform.position;
            anchor.connectedBody = body;
        }

        public void ReleaseFrom(Joint anchor, Vector3 launchVelocity) {
            collider.enabled = true;
            anchor.connectedBody = null;
            body.velocity = launchVelocity + velocityMultiplier * body.velocity;
            body.gameObject.AddComponent<KinematicRigidbody>();
        }

        void OnCollisionEnter(Collision collision) {
            onCollisionEnter?.Invoke(this, collision);
        }
    }
}