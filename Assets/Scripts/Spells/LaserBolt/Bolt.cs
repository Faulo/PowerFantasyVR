using UnityEngine;

namespace PFVR.Spells.LaserBolt {
    [RequireComponent(typeof(Rigidbody))]
    public class Bolt : MonoBehaviour {
        public Vector3 velocity {
            get => GetComponent<Rigidbody>().velocity;
            set => GetComponent<Rigidbody>().velocity = value;
        }

        [SerializeField]
        GameObject explosionPrefab = default;
        [SerializeField, Range(0, 1)]
        float explosionSize = 1;

        void OnCollisionEnter(Collision collision) {
            if ((LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) & LayerMask.GetMask("Default", "Spell", "Obstacle", "Ground", "Enemy")) == 0) {
                return;
            }
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().scaling = explosionSize;
            Destroy(gameObject);
        }
    }
}
