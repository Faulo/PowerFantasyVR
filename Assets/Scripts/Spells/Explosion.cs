using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells {
    [RequireComponent(typeof(ScalableObject))]
    public class Explosion : MonoBehaviour {
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

        public float size {
            get => scale.scaling;
            set => scale.scaling = value;
        }
        private ScalableObject scale => GetComponent<ScalableObject>();

        private ParticleSystem particles => GetComponentInChildren<ParticleSystem>();
        void Start() {
            Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Default", "Obstacle", "Player"))
                .SelectMany(collider => collider.GetComponentsInParent<Rigidbody>())
                .ForAll(body => {
                    var direction = body.transform.position - transform.position + Vector3.up;
                    var force = size * maximumForce * forceOverDistance.Evaluate(direction.magnitude / range);
                    body.AddForce(direction * force, ForceMode.Impulse);
                    body.AddForce(Vector3.up * force, ForceMode.Impulse);
                });
            particles.Play();
            Destroy(gameObject, particles.main.duration);
        }
    }
}