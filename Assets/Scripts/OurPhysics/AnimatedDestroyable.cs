using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Animator))]
    public class AnimatedDestroyable : MonoBehaviour, IDestroyable {
        [SerializeField, Range(1, 100)]
        private float maxHP = 1;
        private float waited = 0.0f;
        public float currentHP {
            get => currentHPCache;
            set {
                if (value != currentHPCache) {
                    if (value < currentHPCache) {
                        animator.SetTrigger("DamageTaken");
                    } else {
                        animator.SetTrigger("DamageHealed");
                    }
                    currentHPCache = value;
                }
            }
        }
        private float currentHPCache;
        public bool isAlive { get; private set; } = true;
        public new Rigidbody rigidbody { get; private set; }
        public Vector3 position => transform.position;

        private Animator animator;

        // Start is called before the first frame update
        void Start() {
            currentHPCache = maxHP;
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            if (isAlive && currentHP <= 0) {
                isAlive = false;
                animator.SetBool("IsAlive", false);
                waited = 0.0f;
            }
            //if(!isAlive && animator.GetAnimatorTransitionInfo(1).duration <= waited)
            //{
            //    Destroy(gameObject);
            //}
            //waited += Time.deltaTime;
        }
    }
}