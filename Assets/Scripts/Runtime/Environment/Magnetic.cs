using UnityEngine;

namespace PFVR.Environment {
    [RequireComponent(typeof(Collider))]
    public sealed class Magnetic : MonoBehaviour {
        [SerializeField]
        Transform root = default;
        [SerializeField]
        LayerMask targetLayer = default;
        [SerializeField, Range(0, 100)]
        float accelerationSpeed = 1;
        [SerializeField, Range(0, 1000)]
        float maximumSpeed = 100;
        Vector3 velocity;

        Transform target;


        void Update() {
            if (target) {
                velocity = Vector3.Lerp(velocity, maximumSpeed * (target.position - root.position).normalized, accelerationSpeed * Time.deltaTime);
                root.position += velocity * Time.deltaTime;
            }
        }
        void OnTriggerEnter(Collider other) {
            if (((1 << other.gameObject.layer) & targetLayer) != 0) {
                target = other.gameObject.transform;
            }
        }
    }
}