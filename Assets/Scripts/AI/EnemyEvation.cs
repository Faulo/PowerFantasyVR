using System;
using UnityEngine;

namespace PFVR.AI
{
    public static class EnemyEvation
    {

        public static Func<Vector3, Vector3, Vector3> FindEvadeBehavior(int evadeDirection)
        {
            switch (evadeDirection)
            {
                case 0:
                    return EvadeMethod90;
                case 1:
                    return EvadeMethodMinus90;
                case 2:
                    return EvadeMethod180;
                default:
                    return EvadeMethod0;
            }
        }

        // Evade in a fixed direction for whole life. Only evade when new player attack. Evade in same direction for whole take.
        private static Vector3 EvadeMethod90(Vector3 playerPosition, Vector3 objectPosition)
        {
            Vector3 evadeVector = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z) - objectPosition;
            return Quaternion.AngleAxis(90, evadeVector) * evadeVector;
        }

        private static Vector3 EvadeMethodMinus90(Vector3 playerPosition, Vector3 objectPosition)
        {
            Vector3 evadeVector = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z) - objectPosition;
            return evadeVector = Quaternion.AngleAxis(-90, evadeVector) * evadeVector;
        }

        private static Vector3 EvadeMethod180(Vector3 playerPosition, Vector3 objectPosition)
        {
            Vector3 evadeVector = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z) - objectPosition;
            return evadeVector = Quaternion.AngleAxis(180, evadeVector) * evadeVector;
        }

        private static Vector3 EvadeMethod0(Vector3 playerPosition, Vector3 objectPosition)
        {
            Vector3 evadeVector = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z) - objectPosition;
            return evadeVector = Quaternion.AngleAxis(0, evadeVector) * evadeVector;
        }
    }
}