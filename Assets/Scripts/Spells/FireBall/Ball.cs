using PFVR.OurPhysics;
using PFVR.Spells.LaserBolt;
using PFVR.Spells.LaserRay;
using Slothsoft.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    public class Ball : ScalableObject, IDestroyable {
        [SerializeField]
        private GameObject regularExplosionPrefab = default;
        [SerializeField]
        private GameObject laserExplosionPrefab = default;

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

        private void Awake() {
            collider = GetComponentInChildren<Collider>();
            rigidbody = GetComponentInChildren<Rigidbody>();
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        private void OnCollisionEnter(Collision collision) {
            currentHP = 0;
        }

        public void RegularExplode() {
            ExplodeWith(regularExplosionPrefab);
        }
        public void LaserExplode() {
            ExplodeWith(laserExplosionPrefab);
        }

        private void ExplodeWith(GameObject prefab) {
            if (isAlive) {
                isAlive = false;
                var explosion = Instantiate(prefab, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().scaling = scaling;
                Destroy(gameObject);
            }
        }
    }
}