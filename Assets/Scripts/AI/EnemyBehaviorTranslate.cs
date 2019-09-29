using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using PFVR.OurPhysics;
using PFVR.Player;

namespace PFVR.AI
{
    /**
     * <summary>Implements the <c>EnemyBehavior</c> base class.</summary>
     **/
    public class EnemyBehaviorTranslate : EnemyBehavior
    {
        /**
         * <summary>Moves swarm object by translation.</summary>
         **/
        public override void MoveObject()
        {
            transform.Translate(finalMovementVector * Time.deltaTime);
        }
    }
}
