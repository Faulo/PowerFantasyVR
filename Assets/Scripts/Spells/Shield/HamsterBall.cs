using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.Shield {
    public class HamsterBall : MonoBehaviour {
        public void Explode() {
            Destroy(gameObject);
        }
        void FixedUpdate() {
            transform.rotation = Quaternion.identity;
        }
    }
}