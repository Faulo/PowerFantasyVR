using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.Disaster {
    /// <summary>
    /// Fires stuff into the game world!
    /// </summary>
    public class ObjectCannon : MonoBehaviour {
        [SerializeField]
        GameObject[] projectilePrefabs = default;

        [SerializeField]
        AnimationCurve projectileScale = default;

        [SerializeField, Range(0, 1000)]
        float launchVelocity = 10;

        [SerializeField, Range(0, 1000)]
        float launchDiffusion = 10;

        [SerializeField, Range(1, 100)]
        float burstInterval = 1;

        [SerializeField, Range(1, 100)]
        float burstCount = 1;

        float burstTimer = 0;
        float burstCountdown;

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
            projectile.transform.position += direction;
            var rigidbody = projectile.GetComponent<Rigidbody>();
            if (rigidbody) {
                rigidbody.velocity = direction;
                rigidbody.drag *= Random.value + 0.5f;
            }
            var scalable = projectile.GetComponent<ScalableObject>();
            if (scalable) {
                scalable.scaling = projectileScale.Evaluate(Random.value);
            }
        }
    }

}