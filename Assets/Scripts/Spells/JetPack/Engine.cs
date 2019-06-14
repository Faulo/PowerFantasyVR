using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.JetPack {
    [RequireComponent(typeof(AudioSource))]
    public class Engine : MonoBehaviour {
        [SerializeField]
        private float maximumVolume = 1;

        [SerializeField]
        private AudioClip startupSound = default;

        [SerializeField]
        private AudioClip continuousSound = default;

        public float propulsion {
            get => propulsionCache;
            set {
                propulsionCache = Mathf.Clamp(value, 0, 1);
                audioSource.volume = maximumVolume * propulsionCache;
            }
        }
        private float propulsionCache;
        private AudioSource audioSource => GetComponent<AudioSource>();
        private Coroutine playSoundsRoutine;
        

        public void TurnOn() {
            gameObject.SetActive(true);
            playSoundsRoutine = StartCoroutine(PlayEngineSounds());
        }

        public void TurnOff() {
            gameObject.SetActive(false);
            if (playSoundsRoutine != null) {
                StopCoroutine(playSoundsRoutine);
            }
            audioSource.Stop();
        }

        private IEnumerator PlayEngineSounds() {
            audioSource.loop = false;
            audioSource.clip = startupSound;
            audioSource.Play();
            while (audioSource.isPlaying) {
                yield return null;
            }

            audioSource.loop = true;
            audioSource.clip = continuousSound;
            audioSource.Play();
            while (audioSource.isPlaying) {
                audioSource.volume = propulsion;
                yield return null;
            }
        }
    }
}