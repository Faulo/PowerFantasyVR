using PFVR.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.OurPhysics {
    public class BasicDestroyable : MonoBehaviour, IDestroyable {
        [SerializeField, Range(1, 100)]
        public float maxHP = 1;
        public float currentHP {
            get => currentHPCache;
            set {
                if (value != currentHPCache) {
                    if (value < currentHPCache) {
                        damageTakenBurster.Burst(currentHPCache - value);
                    }
                    currentHPCache = value;
                }
            }
        }
        private float currentHPCache;
        public bool isAlive { get; private set; } = true;
        public new Rigidbody rigidbody { get; private set; }

        [SerializeField]
        public GameObject damageTakenPrefab = default;
        private ParticleSystemBurster damageTakenBurster;
        [SerializeField]
        public GameObject damageHealedPrefab = default;
        [SerializeField]
        public GameObject deadPrefab = default;


        void Start() {
            currentHPCache = maxHP;
            rigidbody = GetComponent<Rigidbody>();
            damageTakenBurster = Instantiate(damageTakenPrefab, transform).GetComponent<ParticleSystemBurster>();
        }

        void Update() {
            if (isAlive && currentHP <= 0) {
                isAlive = false;
                if (deadPrefab) {
                    AnimateState(deadPrefab);
                }
                Destroy(gameObject);
            }
        }

        private void AnimateState(GameObject prefab) {
            var state = Instantiate(prefab, transform.position, Quaternion.identity);
            state.transform.localScale = transform.localScale;
            if (rigidbody && state.GetComponent<Rigidbody>()) {
                state.GetComponent<Rigidbody>().velocity = rigidbody.velocity;
            }
        }
    }
}