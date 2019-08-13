using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells {
    [RequireComponent(typeof(ScalableObject))]
    public class Explosion : MonoBehaviour {
        public static GameObject Instantiate(GameObject prefab, Vector3 position, float size) {
            var explosion = Instantiate(prefab, position, Quaternion.identity);
            explosion.GetComponent<Explosion>().size = size;
            return explosion;
        }

        [SerializeField]
        private float range = 1;
        [SerializeField]
        private float maximumForce = 1;
        [SerializeField]
        private AnimationCurve forceOverDistance = default;
        [SerializeField]
        private float maximumDamage = 1;
        [SerializeField]
        private AnimationCurve damageOverDistance = default;
        [SerializeField, Range(0, 1)]
        private float upwardsModifier = 0;

        public float size {
            get => scale.scaling;
            set => scale.scaling = value;
        }
        private ScalableObject scale => GetComponent<ScalableObject>();

        private ParticleSystem particles => GetComponentInChildren<ParticleSystem>();

        void Start() {
            var colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Default", "Obstacle", "Player"));
            colliders
                .ForAll(collider => {
                    var direction = collider.transform.position - transform.position + Vector3.up;
                    var force = size * maximumForce * forceOverDistance.Evaluate(direction.magnitude / range);
                    var damage = size * maximumDamage * damageOverDistance.Evaluate(direction.magnitude / range);
                    //Debug.Log(force);
                    collider
                        .GetComponentsInParent<Rigidbody>()
                        .ForAll(body => {
                            body.AddForce(direction * force, ForceMode.Impulse);
                            body.AddForce(Vector3.up * force * upwardsModifier, ForceMode.Impulse);
                        });
                    collider
                        .GetComponentsInParent<IDestroyable>()
                        .Log()
                        .ForAll(destroyable => destroyable.currentHP -= damage);
                });
            particles.Play();
            Destroy(gameObject, particles.main.duration);
        }
    }
}