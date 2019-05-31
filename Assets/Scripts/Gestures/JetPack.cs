using PFVR;
using PFVR.Gestures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public class JetPack : MonoBehaviour, IGestureState {
        [SerializeField]
        private float propulsionForce = 1000;

        public void OnEnter(PlayerBehaviour player) {
            Debug.Log("JetPack" + player.transform.position);
        }
        public void OnExit(PlayerBehaviour player) {
        }
        public void OnUpdate(PlayerBehaviour player) {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * propulsionForce * Time.deltaTime);
        }
    }
}