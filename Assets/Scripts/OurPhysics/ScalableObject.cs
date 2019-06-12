using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class ScalableObject : MonoBehaviour {
        [SerializeField]
        private AnimationCurve scalingCurve = default;

        public float scaling {
            get {
                return currentScaling;
            }
            set {
                currentScaling = value;
                transform.localScale = Vector3.one * scalingCurve.Evaluate(currentScaling);
            }
        }
        private float currentScaling;
    }
}
