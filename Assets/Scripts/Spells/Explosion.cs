using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells {
    public class Explosion : ScalableObject {
        [SerializeField]
        private float maximumRange = 1;
        [SerializeField]
        private float maximumForce = 1;
        [SerializeField, Range(0, 1)]
        private float upwardsModifier = 0;

        [Space]
        [SerializeField]
        private float maximumDamage = 1;
        [SerializeField]
        private AnimationCurve damageOverDistance = default;

        void Start() {
            Physics.OverlapSphere(transform.position, maximumRange * scaling, LayerMask.GetMask("Default", "Obstacle", "Enemy"), QueryTriggerInteraction.Collide)
                .SelectMany(collider => collider.GetComponentsInParent<IDestroyable>())
                .Distinct()
                .ForAll(destroyable => {
                    destroyable.currentHP -= maximumDamage * scaling * damageOverDistance.Evaluate(Vector3.Distance(transform.position, destroyable.position) / maximumRange);
                    //Debug.Log(destroyable + " " + destroyable.currentHP);
                    if (destroyable.rigidbody) {
                        destroyable.rigidbody.AddExplosionForce(maximumForce * scaling, transform.position, maximumRange * scaling, upwardsModifier, ForceMode.VelocityChange);
                    }
                });
            var particleSystem = GetComponentInChildren<ParticleSystem>();
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
}