using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PFVR.OurPhysics;
using PFVR.Player;

namespace PFVR.AI
{
    /**
     * <summary>Implements the <c>EnemyBehavior</c> base class.</summary>
     **/
    public class EnemyBehaviorRigid : EnemyBehavior
    {
        private Rigidbody thisRigidbody;
        
        protected override void Start()
        {
            base.Start();
            thisRigidbody = GetComponent<Rigidbody>();
        }

        /**
         * <summary>Moves swarm object by adding force to the rigidbody.</summary>
         **/
        public override void MoveObject()
        {
            thisRigidbody.AddRelativeForce(finalMovementVector);
        }
    }
}
