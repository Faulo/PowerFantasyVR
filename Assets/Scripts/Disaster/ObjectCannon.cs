using PFVR.OurPhysics;
using PFVR.Spells.FireBall;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Disaster {

    public class ObjectCannon : MonoBehaviour {
        [SerializeField]
        private GameObject[] projectilePrefabs = default;

        [SerializeField]
        private AnimationCurve projectileScale = default;

        [SerializeField, Range(0, 1000)]
        private float launchVelocity = 10;

        [SerializeField, Range(0, 1000)]
        private float launchDiffusion = 10;

        [SerializeField, Range(1, 100)]
        private float burstInterval = 1;

        [SerializeField, Range(1, 100)]
        private float burstCount = 1;

        private float burstTimer = 0;
        private float burstCountdown;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            burstTimer += Time.deltaTime;
            if (burstTimer > burstInterval) {
                burstTimer -= burstInterval;
                burstCountdown += burstCount;
            }
            if (burstCountdown > 0) {
                burstCountdown--;
                Burst();
            }
        }
        void Burst() {
            var direction = transform.forward * launchVelocity + Random.insideUnitSphere * launchDiffusion;
            var projectile = Instantiate(projectilePrefabs.RandomElement(), transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = direction;
            projectile.GetComponent<Rigidbody>().drag *= Random.value + 0.5f;
            projectile.transform.position += direction;
            var ball = projectile.GetComponent<Ball>();
            if (ball) {
                ball.size = projectileScale.Evaluate(Random.value);
            } else {
                projectile.GetComponent<ScalableObject>().scaling = projectileScale.Evaluate(Random.value);
            }
        }
    }

}