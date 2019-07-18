using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Animator))]
    public class Destroyable : MonoBehaviour {
        [SerializeField, Range(1, 100)]
        private float maxHP = 1;
        public float currentHP {
            get => currentHPCache;
            set {
                if (value < currentHPCache) {
                    animator.SetTrigger("DamageTaken");
                }
                currentHPCache = value;
            }
        }
        private float currentHPCache;
        private bool isAlive = true;

        [SerializeField]
        private GameObject destructionPrefab = default;

        private Animator animator;

        // Start is called before the first frame update
        void Start() {
            currentHP = maxHP;
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            if (isAlive && currentHP <= 0) {
                isAlive = false;
                if (destructionPrefab) {
                    var destruction = Instantiate(destructionPrefab, transform.position, Quaternion.identity);
                    var thisBody = GetComponent<Rigidbody>();
                    var thatBody = destruction.GetComponent<Rigidbody>();
                    if (thisBody && thatBody) {
                        thatBody.velocity = thisBody.velocity;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}