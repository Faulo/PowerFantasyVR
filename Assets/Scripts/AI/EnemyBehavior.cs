using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace PFVR.AI
{
    // Additional behavior for enemies (besides homing in on a target)
    public class EnemyBehavior : AIPath
    {
        public override void OnTargetReached()
        {
            base.OnTargetReached();
            //Debug.Log("Gotcha!");
        }
    }
}
