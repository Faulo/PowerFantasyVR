using UnityEngine;

namespace PFVR.AI {
    /**
     * <summary>Implements the <c>EnemyBehavior</c> base class.</summary>
     **/
    public sealed class EnemyBehaviorRigid : EnemyBehavior {
        Rigidbody thisRigidbody;

        protected override void Start() {
            base.Start();
            thisRigidbody = GetComponent<Rigidbody>();
        }

        /**
         * <summary>Moves swarm object by adding force to the rigidbody.</summary>
         **/
        public override void MoveObject() {
            thisRigidbody.AddRelativeForce(finalMovementVector);
        }
    }
}
