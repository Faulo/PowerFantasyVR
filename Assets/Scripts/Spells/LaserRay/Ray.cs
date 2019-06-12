using Slothsoft.UnityExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    [RequireComponent(typeof(LineRenderer))]
    public class Ray : MonoBehaviour {
        private LineRenderer line {
            get => GetComponent<LineRenderer>();
        }
        public void Fire(Vector3 position, Vector3 direction, float range) {
            line.SetPosition(0, position);
            line.SetPosition(1, position + direction * range);

            Physics.RaycastAll(position, direction, range)
                .Select(hit => hit.collider)
                .SelectMany(collider => collider.GetComponents<Rigidbody>())
                .ForAll(body => body.AddForce(direction, ForceMode.VelocityChange));
        }
    }
}