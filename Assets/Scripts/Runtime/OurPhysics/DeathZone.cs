using UnityEngine;

namespace PFVR.OurPhysics {
    public sealed class DeathZone : MonoBehaviour {
        Bounds bounds;
        void Awake() {
            bounds = GetComponent<BoxCollider>().bounds;
        }
        void OnTriggerExit(Collider collider) {
            if (!bounds.Contains(collider.transform.position)) {
                //Debug.Log(collider.gameObject.name + " hit the death zone at " + collider.transform.position + "!");
                Destroy(collider.gameObject);
            }
        }
    }
}