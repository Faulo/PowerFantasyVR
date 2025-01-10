using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.SFX {
    /// <summary>
    /// Plays a sound file upon object instantiation, or possibly at some other point in time.
    /// </summary>
    public sealed class OneShot : MonoBehaviour {
        [SerializeField]
        AudioClip[] audioClips = default;
        [SerializeField]
        bool playOnStart = true;
        [SerializeField]
        bool loop = false;
        [Space]
        [SerializeField, Range(0, 10)]
        float volMin = 1;
        [SerializeField, Range(0, 10)]
        float volMax = 1;
        [Space]
        [SerializeField, Range(0, 10)]
        float pitchMin = 1;
        [SerializeField, Range(0, 10)]
        float pitchMax = 1;
        [Space]
        [SerializeField, Range(0, 1)]
        float spatialBlend = 1;
        [Space]
        [SerializeField]
        AudioSource audioSource = default;

        void Start() {
            if (!audioSource) {
                audioSource = GetComponent<AudioSource>();
                if (!audioSource) {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
            }
            audioSource.spatialBlend = spatialBlend;
            if (playOnStart) {
                Play();
            }
        }


        public void Play() {
            //randomly apply a volume between the volume min max
            audioSource.volume = Random.Range(volMin, volMax);

            //randomly apply a pitch between the pitch min max
            audioSource.pitch = Random.Range(pitchMin, pitchMax);

            // play the sound
            if (loop) {
                audioSource.loop = true;
                audioSource.clip = audioClips.RandomElement();
                audioSource.Play();
            } else {
                audioSource.PlayOneShot(audioClips.RandomElement());
            }
        }

        public void Pause() => audioSource.Pause();
        public void UnPause() => audioSource.UnPause();

        public void IncreasePitch(float delta) {
            pitchMin += delta;
            pitchMax += delta;
        }
    }
}