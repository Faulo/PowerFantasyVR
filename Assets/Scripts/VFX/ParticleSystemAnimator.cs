using UnityEngine;

namespace PFVR.VFX {
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemAnimator : MonoBehaviour {
        [SerializeField]
        int burstCount = 1;

        public bool bursting;

        new ParticleSystem particleSystem;

        void Start() {
            particleSystem = GetComponent<ParticleSystem>();
        }

        void FixedUpdate() {
            if (bursting) {
                bursting = false;
                particleSystem.Emit(burstCount);
            }
        }
    }
}