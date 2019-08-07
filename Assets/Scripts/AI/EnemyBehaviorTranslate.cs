using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using PFVR.OurPhysics;
using PFVR.Player;

namespace PFVR.AI
{
    public class EnemyBehaviorTranslate : EnemyBehavior
    {
        // How to move the object according to the calculated vector
        public override void MoveObject()
        {
            transform.Translate(finalMovementVector * Time.deltaTime);
        }
    }
}
