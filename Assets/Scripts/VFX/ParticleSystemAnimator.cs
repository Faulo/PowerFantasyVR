using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.VFX {
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemAnimator : MonoBehaviour {
        [SerializeField]
        private int burstCount = 1;

        public bool bursting;

        private new ParticleSystem particleSystem;

        private void Start() {
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