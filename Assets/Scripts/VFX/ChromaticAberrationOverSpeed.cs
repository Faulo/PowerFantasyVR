using PFVR.OurPhysics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PFVR.VFX {
    public class ChromaticAberrationOverSpeed : MonoBehaviour {
        [SerializeField, Range(1, 100)]
        private float maxSpeed = 1;
        [SerializeField]
        private CharacterControllerMotor referenceBody = default;

        private ChromaticAberration chromaticAberration;
        void Start() {
            GetComponent<PostProcessVolume>().profile.TryGetSettings(out chromaticAberration);
        }
        void Update() {
            chromaticAberration.intensity.value = referenceBody.velocity.magnitude / maxSpeed;
        }
    }
}