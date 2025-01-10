using System.Linq;
using UnityEngine;

namespace PFVR.VFX {
    public sealed class ParticleSystemDestroyOnFinish : MonoBehaviour {
        [SerializeField]
        GameObject targetObject = default;

        void Start() {
            if (!targetObject) {
                targetObject = gameObject;
            }
            float duration = GetComponentsInChildren<ParticleSystem>()
                .Select(ps => ps.main.duration)
                .Max();
            Destroy(targetObject, duration);
        }
    }
}