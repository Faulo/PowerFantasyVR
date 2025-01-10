using PFVR.Player;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.OurPhysics {
    public sealed class DestroyOnCollision : MonoBehaviour {
        [SerializeField]
        Material destruction = default;

        void OnCollisionEnter(Collision collision) {
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
                    Destroy(body.gameObject, 2);
                }

                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}