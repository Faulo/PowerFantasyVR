using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.VFX {
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailRendererScaleToSpeed : MonoBehaviour {
        [SerializeField, Range(0, 100)]
        private float scaleSpeed = 1;
        [SerializeField, Range(0, 100)]
        private float timeSpeed = 1;
        [SerializeField]
        private AnimationCurve timeOverSpeed = default;

        private TrailRenderer trailRenderer;
        private float maximumTime;

        private Vector3 position;
        private Vector3 velocity;

        private void Start() {
            trailRenderer = GetComponent<TrailRenderer>();
            maximumTime = trailRenderer.time;
            position = transform.position;
            velocity = Vector3.zero;
        }

        void Update() {
            velocity = Vector3.Lerp(velocity, transform.position - position, scaleSpeed * Time.deltaTime);
            position = transform.position;
            trailRenderer.time = Mathf.Lerp(trailRenderer.time, maximumTime * timeOverSpeed.Evaluate(velocity.magnitude), timeSpeed * Time.deltaTime);
        }
    }
}