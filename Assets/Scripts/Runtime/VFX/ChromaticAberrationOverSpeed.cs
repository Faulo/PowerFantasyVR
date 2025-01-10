using PFVR.Player;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PFVR.VFX {
    public sealed class ChromaticAberrationOverSpeed : MonoBehaviour {
        [SerializeField, Range(1, 100)]
        float maxSpeed = 1;
        [SerializeField]
        PlayerBehaviour player = default;

        ChromaticAberration chromaticAberration;
        void Start() {
            GetComponent<PostProcessVolume>().profile.TryGetSettings(out chromaticAberration);
        }
        void Update() {
            chromaticAberration.intensity.value = player.motor.speed / maxSpeed;
        }
    }
}