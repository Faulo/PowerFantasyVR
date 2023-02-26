using System.Collections;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.JetPack {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioLowPassFilter))]
    public class Engine : MonoBehaviour {
        [SerializeField]
        float maximumVolume = 1;

        [SerializeField]
        AnimationCurve cutoffFrequencyOverSpeed = default;

        [SerializeField]
        AudioClip startupSound = default;

        [SerializeField]
        AudioClip continuousSound = default;

        [SerializeField]
        AudioClip shutdownSound = default;

        [SerializeField]
        AudioClip boostSound = default;

        Color particleColor {
            get => particleSystemMain.startColor.color;
            set => particleSystemMain.startColor = value;
        }
        public float propulsion {
            get => propulsionCache;
            set => propulsionCache = Mathf.Clamp01(value);
        }
        float propulsionCache;
        AudioSource audioSource;
        AudioLowPassFilter lowPassFilter;
        PlayerBehaviour player;
        new ParticleSystem particleSystem;
        ParticleSystem.MainModule particleSystemMain;
        Coroutine playSoundsRoutine;

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
        bool isTurnedOnCache;

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
        bool isBoostingCache = false;

        void Awake() {
            audioSource = GetComponent<AudioSource>();
            lowPassFilter = GetComponent<AudioLowPassFilter>();
            particleSystem = GetComponentInChildren<ParticleSystem>();
            particleSystemMain = particleSystem.main;
            player = GetComponentInParent<PlayerBehaviour>();
        }

        IEnumerator PlayEngineSounds() {
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