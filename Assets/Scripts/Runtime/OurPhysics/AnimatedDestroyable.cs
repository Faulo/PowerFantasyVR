using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(Animator))]
    public sealed class AnimatedDestroyable : MonoBehaviour, IDestroyable {
        [SerializeField, Range(1, 1000)]
        float maxHP = 1;
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
        float currentHPCache;
        public bool isAlive { get; private set; } = true;
        public new Rigidbody rigidbody { get; private set; }
        public Vector3 position => transform.position;

        Animator animator;

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
            }
        }
    }
}