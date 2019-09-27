using System;
using UnityEngine;

namespace PFVR.AI
{
    public static class EnemyEvasion
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
            return Rotate(playerPosition, objectPosition, 90);
        }

        private static Vector3 EvadeMethodMinus90(Vector3 playerPosition, Vector3 objectPosition)
        {
            return Rotate(playerPosition, objectPosition, -90);
        }

        private static Vector3 EvadeMethod180(Vector3 playerPosition, Vector3 objectPosition)
        {
            return Rotate(playerPosition, objectPosition, 180);
        }

        private static Vector3 EvadeMethod0(Vector3 playerPosition, Vector3 objectPosition) {
            return Rotate(playerPosition, objectPosition, 0);
        }

        private static Vector3 Rotate(Vector3 playerPosition, Vector3 objectPosition, float angle) {
            Vector3 evadeVector = playerPosition - objectPosition;
            Vector3 norm = Vector3.Cross(playerPosition, objectPosition);
            return (Quaternion.AngleAxis(angle, evadeVector) * norm).normalized;
        }
    }
}