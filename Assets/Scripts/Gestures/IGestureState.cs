using PFVR.DataModels;
using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public interface IGestureState {
        void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand);
        void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand);
        void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand);
    }
}