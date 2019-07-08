using Slothsoft.UnityExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    [RequireComponent(typeof(LineRenderer))]
    public class LineRay : MonoBehaviour, IRay {
        private LineRenderer line => GetComponent<LineRenderer>();
        public void Fire(Vector3 position, Vector3 direction, float range, float force) {
            UpdateRay(position, direction, range, force);
        }

        public void Stop() {
            Destroy(gameObject);
        }

        public void UpdateRay(Vector3 position, Vector3 direction, float range, float force) {
            line.SetPosition(0, position);
            line.SetPosition(1, position + direction * range);

            Physics.RaycastAll(position, direction, range, LayerMask.GetMask("Default", "Obstacle", "Player"))
                .SelectMany(hit => hit.collider.GetComponentsInParent<Rigidbody>())
                .ForAll(body => {
                    body.AddTorque(Vector3.one * force * 1000, ForceMode.VelocityChange);
                });
        }
    }
}