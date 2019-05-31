using PFVR.Tracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public interface IGestureState {
        void OnExit(PlayerBehaviour player, Hand hand);
        void OnEnter(PlayerBehaviour player, Hand hand);
        void OnUpdate(PlayerBehaviour player, Hand hand);
    }
}