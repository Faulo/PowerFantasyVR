using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PFVR.VFX.Player {
    public class ChromaticAberrationOverSpeed : MonoBehaviour {
        [SerializeField, Range(1, 100)]
        private float maxSpeed = 1;
        private new Rigidbody rigidbody;
        private ChromaticAberration chromaticAberration;
        // Start is called before the first frame update
        void Start() {
            rigidbody = GetComponentInParent<Rigidbody>();
            GetComponent<PostProcessVolume>().profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        }

        // Update is called once per frame
        void Update() {
            chromaticAberration.intensity.value = rigidbody.velocity.magnitude / maxSpeed;
        }
    }
}