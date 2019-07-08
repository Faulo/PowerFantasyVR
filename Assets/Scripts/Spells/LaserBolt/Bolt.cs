using PFVR.OurPhysics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.LaserBolt {
    [RequireComponent(typeof(Rigidbody))]
    public class Bolt : MonoBehaviour {
        public Vector3 velocity {
            get => GetComponent<Rigidbody>().velocity;
            set => GetComponent<Rigidbody>().velocity = value;
        }

        [SerializeField]
        private GameObject explosionPrefab = default;
        [SerializeField, Range(0, 1)]
        private float explosionSize = 1;

        void OnCollisionEnter(Collision collision) {
            if ((LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) & LayerMask.GetMask("Default", "Spell", "Obstacle", "Ground")) == 0) {
                return;
            }
            Explosion.Instantiate(explosionPrefab, transform.position, explosionSize);
            Destroy(gameObject);
        }
    }
}
