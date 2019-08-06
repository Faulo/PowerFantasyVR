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

        public void Start() {
            //transform.SetScaleZ(rayRange);
        }

        private void OnTriggerStay(Collider other) {
            other.GetComponentsInParent<Destroyable>()
                .ForAll(destroyable => destroyable.currentHP -= rayDamagePerSecond * Time.deltaTime);
            other.GetComponentsInParent<FireBall.Ball>()
                .ForAll(ball => ball.LaserExplode());
        }

        private void Update() {
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