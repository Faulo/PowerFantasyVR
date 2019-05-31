using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR {

    public class Spawner : MonoBehaviour {
        [SerializeField]
        private float delay = 1;

        [SerializeField]
        private GameObject entity;

        private float countdown;

        // Update is called once per frame
        void Update() {
            if (entity) {
                countdown += Time.deltaTime;
                while (countdown > delay) {
                    countdown -= delay;
                    Instantiate(entity, transform);
                }
            }
        }
    }
}