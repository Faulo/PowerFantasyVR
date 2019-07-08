using Slothsoft.UnityExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    public class CylinderRay : MonoBehaviour, IRay {
        public void Fire(Vector3 position, Vector3 direction, float range, float force, float lifetime) {
            transform.position = position;
            transform.LookAt(position + direction);
            transform.SetScaleZ(range);
            Destroy(gameObject, lifetime / 1000);
        }
        public void UpdateRay(Vector3 position, Vector3 direction, float range, float force) {
            transform.position = position;
            transform.LookAt(position + direction);
            transform.SetScaleZ(range);
        }
        public void Stop() {
            Destroy(gameObject);
        }
    }
}