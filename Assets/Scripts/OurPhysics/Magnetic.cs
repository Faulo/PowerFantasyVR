using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Environment {
    [RequireComponent(typeof(Collider))]
    public class Magnetic : MonoBehaviour {
        [SerializeField]
        private Transform root = default;
        [SerializeField]
        private LayerMask targetLayer = default;
        [SerializeField, Range(0, 100)]
        private float accelerationSpeed = 1;
        [SerializeField, Range(0, 1000)]
        private float maximumSpeed = 100;
        private Vector3 velocity;

        private Transform target;


        void Update() {
            if (target) {
                velocity = Vector3.Lerp(velocity, maximumSpeed * (target.position - root.position).normalized, accelerationSpeed * Time.deltaTime);
                root.position += velocity * Time.deltaTime;
            }
        }
        private void OnTriggerEnter(Collider other) {
            if (((1 << other.gameObject.layer) & targetLayer) != 0) {
                target = other.gameObject.transform;
            }
        }
    }
}