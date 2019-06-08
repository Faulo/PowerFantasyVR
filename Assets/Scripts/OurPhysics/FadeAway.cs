using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class FadeAway : MonoBehaviour {
        [SerializeField]
        [Range(0, 10)]
        private float lifetime = 1;

        [SerializeField]
        private AnimationCurve alphaOverTime;

        private float timer = 0;
        private IEnumerable<Renderer> renderers;

        void Start() {
            renderers = GetComponents<Renderer>();
            Debug.Log(renderers.Count());
        }
        void FixedUpdate() {
        }
    }
}
