using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.Shield {
    public class HamsterBall : MonoBehaviour, IShield {
        public event Action<GameObject> onCollision;

        public void Explode() {
            Destroy(gameObject);
        }
        void OnCollisionEnter(Collision collision) {
            if ((LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) & LayerMask.GetMask("Default", "Spell", "Obstacle", "Ground", "Enemy")) == 0) {
                return;
            }
            onCollision?.Invoke(collision.gameObject);
        }
    }
}