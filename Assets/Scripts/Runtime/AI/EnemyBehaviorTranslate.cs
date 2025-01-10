using UnityEngine;

namespace PFVR.AI {
    /**
     * <summary>Implements the <c>EnemyBehavior</c> base class.</summary>
     **/
    public sealed class EnemyBehaviorTranslate : EnemyBehavior {
        /**
         * <summary>Moves swarm object by translation.</summary>
         **/
        public override void MoveObject() {
            transform.Translate(finalMovementVector * Time.deltaTime);
        }
    }
}
