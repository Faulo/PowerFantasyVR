using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    public class BasicRay : MonoBehaviour {
        [SerializeField, Range(1, 1000)]
        private float rayRange = 1;
        [SerializeField, Range(0, 100)]
        private float rayDamagePerSecond = 0;

        private HashSet<IDestroyable> destroyables = new HashSet<IDestroyable>();

        public bool isCutting => destroyables.Count > 0;

        public void Start() {
            transform.SetScaleZ(rayRange);
        }

        private void OnTriggerEnter(Collider other) {
            other.GetComponentsInParent<IDestroyable>()
                .ForAll(destroyable => destroyables.Add(destroyable));
            other.GetComponentsInParent<FireBall.Ball>()
                .ForAll(ball => ball.LaserExplode());
        }
        private void OnTriggerStay(Collider other) {
            other.GetComponentsInParent<IDestroyable>()
                .ForAll(destroyable => destroyables.Add(destroyable));
        }
        private void OnTriggerExit(Collider other) {
            other.GetComponentsInParent<IDestroyable>()
                .ForAll(destroyable => destroyables.Remove(destroyable));
        }

        private void Update() {
            destroyables.RemoveWhere(destroyable => !destroyable.isAlive);
            destroyables.ForAll(destroyable => {
                var damage = rayDamagePerSecond * Time.deltaTime;
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