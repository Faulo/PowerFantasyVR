using PFVR;
using PFVR.Gestures;
using PFVR.Tracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public class Fist : MonoBehaviour, IGestureState {
        public void OnEnter(PlayerBehaviour player, Hand hand) {
        }
        public void OnExit(PlayerBehaviour player, Hand hand) {
        }
        public void OnUpdate(PlayerBehaviour player, Hand hand) {
            player.rigidbody.velocity *= 0.25f;
        }
    }
}