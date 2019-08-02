using PFVR.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.JetPack {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioLowPassFilter))]
    public class Engine : MonoBehaviour {
        [SerializeField]
        private float maximumVolume = 1;

        [SerializeField]
        private AnimationCurve cutoffFrequencyOverSpeed = default;

        [SerializeField]
        private AudioClip startupSound = default;

        [SerializeField]
        private AudioClip continuousSound = default;

        [SerializeField]
        private AudioClip shutdownSound = default;

        public float propulsion {
            get => propulsionCache;
            set => propulsionCache = Mathf.Clamp(value, 0, 1);
        }
        private float propulsionCache;
        private AudioSource audioSource => GetComponent<AudioSource>();
        private AudioLowPassFilter lowPassFilter => GetComponent<AudioLowPassFilter>();
        private PlayerBehaviour player => GetComponentInParent<PlayerBehaviour>();
        private new ParticleSystem particleSystem => GetComponentInChildren<ParticleSystem>();
        private Coroutine playSoundsRoutine;
        private bool turnedOn = false;

        public void TurnOn() {
            turnedOn = true;
            particleSystem.Play();
            if (playSoundsRoutine != null) {
                StopCoroutine(playSoundsRoutine);
            }
            playSoundsRoutine = StartCoroutine(PlayEngineSounds());
        }

        public void TurnOff() {
            turnedOn = false;
            particleSystem.Stop();
        }

        private IEnumerator PlayEngineSounds() {
            audioSource.loop = false;
            audioSource.clip = startupSound;
            audioSource.Play();
            while (turnedOn && audioSource.isPlaying) {
                yield return null;
            }

            audioSource.loop = true;
            audioSource.clip = continuousSound;
            audioSource.Play();
            while (turnedOn) {
                yield return null;
            }

            audioSource.loop = false;
            audioSource.clip = shutdownSound;
            audioSource.Play();

            while (audioSource.isPlaying) {
                propulsion *= 0.75f;
                yield return null;
            }

            audioSource.Stop();
            playSoundsRoutine = null;
        }

        void Update() {
            audioSource.volume = maximumVolume * propulsion;
            lowPassFilter.cutoffFrequency = cutoffFrequencyOverSpeed.Evaluate(player.motor.speed);
        }
    }
}