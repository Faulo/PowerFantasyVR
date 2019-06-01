using PFVR;
using PFVR.DataModels;
using PFVR.Gestures;
using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public class Nothing : MonoBehaviour, IGestureState {
        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
    }
}