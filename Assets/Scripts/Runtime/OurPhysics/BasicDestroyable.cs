using PFVR.VFX;
using UnityEngine;

namespace PFVR.OurPhysics {
    public sealed class BasicDestroyable : MonoBehaviour, IDestroyable {
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
        float currentHPCache;
        public bool isAlive { get; private set; } = true;
        public new Rigidbody rigidbody { get; private set; }
        public Vector3 position => transform.position;

        [SerializeField]
        public GameObject damageTakenPrefab = default;
        ParticleSystemBurster damageTakenBurster;
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

        void AnimateState(GameObject prefab) {
            var state = Instantiate(prefab, transform.position, Quaternion.identity);
            state.transform.localScale = transform.localScale;
            if (rigidbody && state.GetComponent<Rigidbody>()) {
                state.GetComponent<Rigidbody>().velocity = rigidbody.velocity;
            }
        }
    }
}