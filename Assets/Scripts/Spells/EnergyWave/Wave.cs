using PFVR.OurPhysics;
using PFVR.Spells.LaserBolt;
using PFVR.Spells.LaserRay;
using Slothsoft.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.EnergyWave {
    [RequireComponent(typeof(ScalableObject))]
    public class Wave : MonoBehaviour {
        [SerializeField]
        private GameObject regularExplosionPrefab = default;

        public float size {
            get => scale.scaling;
            set => scale.scaling = value;
        }

        private ScalableObject scale => GetComponent<ScalableObject>();
        public KinematicRigidbody body => GetComponent<KinematicRigidbody>();
        private new Collider collider => GetComponent<Collider>();

        public bool explodable {
            get => collider.enabled;
            set => collider.enabled = value;
        }

        void OnCollisionEnter(Collision collision) {
            if ((LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) & LayerMask.GetMask("Default", "Spell", "Obstacle", "Ground")) == 0) {
                return;
            }
            Explode();
        }
        public void Explode() {
            Explosion.Instantiate(regularExplosionPrefab, transform.position, size);
            Destroy(gameObject);
        }
    }
}