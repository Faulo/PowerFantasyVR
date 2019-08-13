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

        [SerializeField]
        private AudioClip boostSound = default;

        private Color particleColor {
            get => particleSystemMain.startColor.color;
            set => particleSystemMain.startColor = value;
        }
        public float propulsion {
            get => propulsionCache;
            set => propulsionCache = Mathf.Clamp01(value);
        }
        private float propulsionCache;
        private AudioSource audioSource;
        private AudioLowPassFilter lowPassFilter;
        private PlayerBehaviour player;
        private new ParticleSystem particleSystem;
        private ParticleSystem.MainModule particleSystemMain;
        private Coroutine playSoundsRoutine;

        public bool isTurnedOn {
            get => isTurnedOnCache;
            set {
                if (isTurnedOnCache != value) {
                    isTurnedOnCache = value;
                    if (value) {
                        particleSystem.Play();
                        if (playSoundsRoutine != null) {
                            StopCoroutine(playSoundsRoutine);
                        }
                        playSoundsRoutine = StartCoroutine(PlayEngineSounds());
                    } else {
                        particleSystem.Stop();
                    }
                }
            }
        }
        private bool isTurnedOnCache;

        public bool isBoosting {
            get => isBoostingCache;
            set {
                if (isBoostingCache != value) {
                    isBoostingCache = value;
                    if (value) {
                        particleColor = Color.red;
                        audioSource.PlayOneShot(boostSound);
                    } else {
                        particleColor = Color.white;
                    }
                }
            }
        }
        private bool isBoostingCache = false;

        private void Awake() {
            audioSource = GetComponent<AudioSource>();
            lowPassFilter = GetComponent<AudioLowPassFilter>();
            particleSystem = GetComponentInChildren<ParticleSystem>();
            particleSystemMain = particleSystem.main;
            player = GetComponentInParent<PlayerBehaviour>();
        }

        private IEnumerator PlayEngineSounds() {
            audioSource.loop = false;
            audioSource.clip = startupSound;
            audioSource.Play();
            while (isTurnedOn && audioSource.isPlaying) {
                yield return null;
            }

            audioSource.loop = true;
            audioSource.clip = continuousSound;
            audioSource.Play();
            while (isTurnedOn) {
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