using PFVR.OurPhysics;
using PFVR.Spells.FireBall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Disaster {

    public class ObjectCannon : MonoBehaviour {
        [SerializeField]
        private GameObject projectilePrefab = default;

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
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = direction;
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