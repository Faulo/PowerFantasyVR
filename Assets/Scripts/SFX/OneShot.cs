using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.SFX {
    public class OneShot : MonoBehaviour {
        [SerializeField]
        private AudioClip[] audioClips = default;
        [SerializeField]
        private bool playOnAwake;
        [Space]
        [SerializeField, Range(0, 1)]
        private float volMin = 1;
        [SerializeField, Range(0, 1)]
        private float volMax = 1;
        [Space]
        [SerializeField, Range(0, 10)]
        private float pitchMin = 1;
        [SerializeField, Range(0, 10)]
        private float pitchMax = 1;
        [Space]
        [SerializeField]
        private AudioSource audioSource;

        void Awake() {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource) {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        void Start() {
            if (playOnAwake) {
                Play();
            }
        }


        public void Play() {
            //randomly apply a volume between the volume min max
            audioSource.volume = Random.Range(volMin, volMax);

            //randomly apply a pitch between the pitch min max
            audioSource.pitch = Random.Range(pitchMin, pitchMax);

            // play the sound
            audioSource.PlayOneShot(audioClips.RandomElement());
        }

        public void Pause() => audioSource.Pause();
        public void UnPause() => audioSource.UnPause();
    }
}