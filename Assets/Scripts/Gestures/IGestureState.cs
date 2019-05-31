using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public interface IGestureState {
        void OnExit(PlayerBehaviour player);
        void OnEnter(PlayerBehaviour player);
        void OnUpdate(PlayerBehaviour player);
    }
}