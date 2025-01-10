using System.Collections.Generic;
using System.Linq;
using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    public sealed class BasicRay : MonoBehaviour {
        [SerializeField, Range(1, 1000)]
        float rayRange = 1;
        [SerializeField, Range(0, 100)]
        float rayDamagePerSecond = 0;

        HashSet<IDestroyable> destroyables = new HashSet<IDestroyable>();

        public bool isCutting => destroyables.Count > 0;

        public void Start() {
            transform.localScale = transform.localScale.WithZ(rayRange);
        }

        void OnTriggerEnter(Collider other) {
            other.GetComponentsInParent<IDestroyable>()
                .ForAll(destroyable => destroyables.Add(destroyable));
            other.GetComponentsInParent<FireBall.Ball>()
                .ForAll(ball => ball.LaserExplode());
        }
        void OnTriggerStay(Collider other) {
            other.GetComponentsInParent<IDestroyable>()
                .ForAll(destroyable => destroyables.Add(destroyable));
        }
        void OnTriggerExit(Collider other) {
            other.GetComponentsInParent<IDestroyable>()
                .ForAll(destroyable => destroyables.Remove(destroyable));
        }

        void Update() {
            destroyables.RemoveWhere(destroyable => !destroyable.isAlive);
            destroyables.ForAll(destroyable => {
                float damage = rayDamagePerSecond * Time.deltaTime;
                destroyable.currentHP -= damage;
                if (destroyable.rigidbody) {
                    destroyable.rigidbody.AddForce(damage * UnityEngine.Random.insideUnitSphere, ForceMode.VelocityChange);
                }
            });
            /*
            if (closestCollider) {
                var ray = new Ray(transform.position, transform.forward * rayRange);
                if (closestCollider.bounds.IntersectRay(ray, out var distance)) {
                    transform.SetScaleZ(-distance);
                    //Debug.Log(distance);
                    //pointTransform.transform.position = transform.position + transform.forward * distance;
                    //Debug.Log(pointTransform.localPosition);
                    //pointTransform.localPosition = Vector3.up * distance / rayRange;
                } else {
                    transform.SetScaleZ(rayRange);
                    //pointTransform.localPosition = -Vector3.up;
                }
            } else {
                transform.SetScaleZ(rayRange);
                //pointTransform.localPosition = Vector3.up;
            }
            //*/
        }
    }
}