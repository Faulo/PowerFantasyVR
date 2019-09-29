using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.AI
{
    /**
     * <summary>This class makes sure the explosion animation is played when the object ist destroyed.</summary>
     **/
    public class EnemyExplodeStateMachine : StateMachineBehaviour
    {
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("EnemyExploded", true);
        }
    }
}