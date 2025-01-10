using UnityEngine;

namespace PFVR.VFX {
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class ParticleSystemBurster : MonoBehaviour {
        [SerializeField, Range(0, 10)]
        public float burstMultiplier = 1;

        new ParticleSystem particleSystem;

        void Start() {
            particleSystem = GetComponent<ParticleSystem>();
        }

        public void Burst(float amount) {
            particleSystem.Emit(Mathf.CeilToInt(amount * burstMultiplier));
        }
    }
}