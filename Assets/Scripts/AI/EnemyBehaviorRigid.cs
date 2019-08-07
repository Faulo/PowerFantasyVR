using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using PFVR.OurPhysics;
using PFVR.Player;

namespace PFVR.AI
{
    // Additional behavior for enemies (besides homing in on a target)
    public class EnemyBehaviorRigid : EnemyBehavior
    {
        // Transformation calculation
        private Rigidbody thisRigidbody;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            thisRigidbody = GetComponent<Rigidbody>();
        }
        
        public override void MoveObject()
        {
            thisRigidbody.AddRelativeForce(finalMovementVector);
        }
    }
}
