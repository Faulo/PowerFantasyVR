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
            Destroy(gameObject, lifetime);
        }
    }
}