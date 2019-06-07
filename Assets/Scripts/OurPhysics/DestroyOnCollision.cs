using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.OurPhysics {
    public class DestroyOnCollision : MonoBehaviour {
        [SerializeField]
        private Material destruction = default;

        private void OnCollisionEnter(Collision collision) {
            var body = collision.rigidbody;
            if (body != null) {
                if (body.GetComponentInParent<PlayerBehaviour>()) {
                    return;
                }
                if (destruction != null) {
                    body.GetComponents<MeshRenderer>()
                        .ForAll(renderer => {
                            renderer.material = destruction;
                        });
                }
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}