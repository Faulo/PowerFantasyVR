using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.VFX {
    public class ParticleSystemDestroyOnFinish : MonoBehaviour {
        [SerializeField]
        private GameObject targetObject = default;

        void Start() {
            if (!targetObject) {
                targetObject = gameObject;
            }
            var duration = GetComponentsInChildren<ParticleSystem>()
                .Select(ps => ps.main.duration)
                .Max();
            Destroy(targetObject, duration);
        }
    }
}