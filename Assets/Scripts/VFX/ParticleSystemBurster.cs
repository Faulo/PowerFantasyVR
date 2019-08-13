using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.VFX {
    [ExecuteInEditMode, RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemBurster : MonoBehaviour {
        [SerializeField, Range(0, 10)]
        public float burstMultiplier = 1;

        private new ParticleSystem particleSystem;

        private void Start() {
            particleSystem = GetComponent<ParticleSystem>();
        }

        public void Burst(float amount) {
            particleSystem.Emit(Mathf.CeilToInt(amount * burstMultiplier));
        }
    }
}