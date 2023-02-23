using PFVR.OurPhysics;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    public class Ball : ScalableObject, IDestroyable {
        [SerializeField]
        GameObject regularExplosionPrefab = default;
        [SerializeField]
        GameObject laserExplosionPrefab = default;

        public new Collider collider { get; private set; }

        public new Rigidbody rigidbody { get; private set; }

        public TrailRenderer trailRenderer { get; private set; }

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

        public bool isAlive { get; private set; } = true;
        public Vector3 position => transform.position;

        void Awake() {
            collider = GetComponentInChildren<Collider>();
            rigidbody = GetComponentInChildren<Rigidbody>();
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        void Update() {
            trailRenderer.startWidth = scaledScaling;
            trailRenderer.endWidth = scaledScaling;
        }

        void OnCollisionEnter(Collision collision) {
            currentHP = 0;
        }

        public void RegularExplode() {
            ExplodeWith(regularExplosionPrefab);
        }
        public void LaserExplode() {
            ExplodeWith(laserExplosionPrefab);
        }

        void ExplodeWith(GameObject prefab) {
            if (isAlive) {
                isAlive = false;
                var explosion = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Explosion>();
                explosion.maximumScaling = maximumScaling;
                explosion.scaling = scaling;
                Destroy(gameObject);
            }
        }
    }
}