using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.SFX {
    public class OneShot : MonoBehaviour {
        [SerializeField]
        private AudioClip[] audioClips = default;
        [SerializeField]
        private bool playOnStart = true;
        [SerializeField]
        private bool loop = false;
        [Space]
        [SerializeField, Range(0, 10)]
        private float volMin = 1;
        [SerializeField, Range(0, 10)]
        private float volMax = 1;
        [Space]
        [SerializeField, Range(0, 10)]
        private float pitchMin = 1;
        [SerializeField, Range(0, 10)]
        private float pitchMax = 1;
        [Space]
        [SerializeField, Range(0, 1)]
        private float spatialBlend = 1;
        [Space]
        [SerializeField]
        private AudioSource audioSource = default;

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