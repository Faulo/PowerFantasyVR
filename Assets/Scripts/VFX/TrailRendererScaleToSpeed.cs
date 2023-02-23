using UnityEngine;

namespace PFVR.VFX {
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailRendererScaleToSpeed : MonoBehaviour {
        [SerializeField, Range(0, 100)]
        float scaleSpeed = 1;
        [SerializeField, Range(0, 100)]
        float timeSpeed = 1;
        [SerializeField]
        AnimationCurve timeOverSpeed = default;

        TrailRenderer trailRenderer;
        float maximumTime;

        Vector3 position;
        Vector3 velocity;

        void Start() {
            trailRenderer = GetComponent<TrailRenderer>();
            maximumTime = trailRenderer.time;
            position = transform.position;
            velocity = Vector3.zero;
        }

        void Update() {
            velocity = Vector3.Lerp(velocity, transform.position - position, scaleSpeed * Time.deltaTime);
            position = transform.position;
            trailRenderer.time = Mathf.Lerp(trailRenderer.time, maximumTime * timeOverSpeed.Evaluate(velocity.magnitude), timeSpeed * Time.deltaTime);
            trailRenderer.emitting = trailRenderer.time > Mathf.Epsilon;
        }
    }
}