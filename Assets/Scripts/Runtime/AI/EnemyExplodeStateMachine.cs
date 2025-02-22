﻿using UnityEngine;

namespace PFVR.AI {
    /**
     * <summary>This class makes sure the explosion animation is played when the object ist destroyed.</summary>
     **/
    public sealed class EnemyExplodeStateMachine : StateMachineBehaviour {
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.SetBool("EnemyExploded", true);
        }
    }
}