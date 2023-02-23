using System;
using UnityEngine;

namespace PFVR.AI {
    /**
     * <summary>Static class for returning one of four possible methods for calculating an evasion direction.</summary>
     **/
    public static class EnemyEvasion {
        /**
         * <summary>Decides which method for calculating an evasion direction to return.</summary>
         * <param name="evadeDirection">Integer value between 1 and 4 for deciding which way to evade. Is set in the calling class once per object.</param>
         * <returns>The Function for calculating an evade direction vector with the object position and player position as input and the new Vector3 as output.</returns>
         **/
        public static Func<Vector3, Vector3, Vector3> FindEvadeBehavior(int evadeDirection) {
            switch (evadeDirection) {
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
        static Vector3 EvadeMethod90(Vector3 playerPosition, Vector3 objectPosition) {
            return Rotate(playerPosition, objectPosition, 90);
        }

        static Vector3 EvadeMethodMinus90(Vector3 playerPosition, Vector3 objectPosition) {
            return Rotate(playerPosition, objectPosition, -90);
        }

        static Vector3 EvadeMethod180(Vector3 playerPosition, Vector3 objectPosition) {
            return Rotate(playerPosition, objectPosition, 180);
        }

        static Vector3 EvadeMethod0(Vector3 playerPosition, Vector3 objectPosition) {
            return Rotate(playerPosition, objectPosition, 0);
        }

        static Vector3 Rotate(Vector3 playerPosition, Vector3 objectPosition, float angle) {
            var evadeVector = playerPosition - objectPosition;
            var norm = Vector3.Cross(playerPosition, objectPosition);
            return (Quaternion.AngleAxis(angle, evadeVector) * norm).normalized;
        }
    }
}