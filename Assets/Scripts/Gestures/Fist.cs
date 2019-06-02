using PFVR;
using PFVR.DataModels;
using PFVR.Gestures;
using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public class Fist : MonoBehaviour, IGestureState {
        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(true);
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(false);
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            player.rigidbody.velocity *= 0.25f;
        }
    }
}