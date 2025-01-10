using PFVR.OurPhysics;
using UnityEngine;

namespace PFVR.Spells.EnergyWave {
    public class Wave : ScalableObject, IDestroyable {
        [SerializeField]
        GameObject regularExplosionPrefab = default;

        public new Rigidbody rigidbody { get; private set; }
        public new Collider collider { get; private set; }

        public bool explodable {
            get => collider.enabled;
            set => collider.enabled = value;
        }

        public float currentHP {
            get => 0;
            set {
                if (value <= 0) {
                    RegularExplode();
                }
            }
        }

        public bool isAlive { get; private set; }
        public Vector3 position => transform.position;

        protected virtual void Awake() {
            collider = GetComponentInChildren<Collider>();
            rigidbody = GetComponentInChildren<Rigidbody>();
        }

        protected virtual void OnTriggerEnter(Collider other) {
            currentHP = 0;
        }

        public void RegularExplode() {
            ExplodeWith(regularExplosionPrefab);
        }

        void ExplodeWith(GameObject prefab) {
            if (isAlive) {
                isAlive = false;
                var explosion = Instantiate(prefab, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().scaling = scaling;
                Destroy(gameObject);
            }
        }
    }
}