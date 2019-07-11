using PFVR.OurPhysics;
using PFVR.Spells.LaserBolt;
using PFVR.Spells.LaserRay;
using Slothsoft.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    [RequireComponent(typeof(ScalableObject))]
    public class Ball : MonoBehaviour {
        [SerializeField, Range(1, 10)]
        private float velocityMultiplier = 1;

        [SerializeField]
        private GameObject regularExplosionPrefab = default;

        [SerializeField]
        private GameObject mergeExplosionPrefab = default;

        [SerializeField]
        private GameObject laserExplosionPrefab = default;

        public float size {
            get => scale.scaling;
            set => scale.scaling = value;
        }

        private ScalableObject scale => GetComponent<ScalableObject>();
        public Rigidbody body => GetComponentInChildren<Rigidbody>();
        private new Collider collider => GetComponentInChildren<Collider>();

        public bool explodable {
            get => collider.enabled;
            set => collider.enabled = value;
        }

        public void ConnectTo(Joint anchor) {
            collider.enabled = false;
            transform.position = anchor.transform.position;
            anchor.connectedBody = body;
        }

        public void ReleaseFrom(Joint anchor) {
            collider.enabled = true;
            anchor.connectedBody = null;
            body.drag = 0;
            body.velocity *= velocityMultiplier;
            body.gameObject.AddComponent<KinematicRigidbody>();
        }

        void OnCollisionEnter(Collision collision) {
            if ((LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) & LayerMask.GetMask("Default", "Spell", "Obstacle", "Ground")) == 0) {
                return;
            }
            var ball = collision.gameObject.GetComponentInParent<Ball>();
            if (ball != null) {
                var explosionPosition = (transform.position + ball.transform.position) / 2;
                var explosionSize = (size + ball.size) / 2;
                Explosion.Instantiate(mergeExplosionPrefab, explosionPosition, explosionSize);
                Destroy(gameObject);
                Destroy(ball.gameObject);
                return;
            }
            var laser = collision.gameObject.GetComponentInParent<BasicRay>();
            if (laser != null) {
                LaserExplode();
                Destroy(gameObject);
                return;
            }
            var bolt = collision.gameObject.GetComponentInParent<Bolt>();
            if (bolt != null) {
                LaserExplode();
                return;
            }
            Explode();
        }

        /*
        void Update() {
            if (explodable) {
                Physics.OverlapSphere(transform.position, mergeRange, LayerMask.GetMask("Spell"))
                    .SelectMany(collider => collider.GetComponents<Ball>())
                    .Where(ball => ball.explodable && ball != this)
                    .ForAll(ball => {
                        var position = (transform.position + ball.transform.position) / 2;
                        var explosion = Instantiate(mergeExplosionPrefab, position, Quaternion.identity).GetComponent<Explosion>();
                        explosion.size = (size + ball.size) / 2;
                        Destroy(gameObject);
                        Destroy(ball.gameObject);
                    });
            }
        }
        //*/
        public void Explode() {
            Explosion.Instantiate(regularExplosionPrefab, transform.position, size);
            Destroy(gameObject);
        }
        public void LaserExplode() {
            Explosion.Instantiate(laserExplosionPrefab, transform.position, size);
            Destroy(gameObject);
        }
    }
}