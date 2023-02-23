using UnityEngine;

namespace PFVR.OurPhysics {
    public class ScalableObject : MonoBehaviour {
        [SerializeField, Range(0, 100)]
        public float maximumScaling = 1;
        [SerializeField]
        AnimationCurve scalingCurve = default;

        public float scaling {
            get => scalingCache;
            set {
                scalingCache = Mathf.Clamp01(value);
                transform.localScale = Vector3.one * scaledScaling;
            }
        }
        float scalingCache;

        public float scaledScaling => maximumScaling * scalingCurve.Evaluate(scalingCache);
    }
}
