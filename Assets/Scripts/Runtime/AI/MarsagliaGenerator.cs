using UnityEngine;

namespace PFVR.AI {
    // Class created by Robin Daraban and permittedly used
    public sealed class MarsagliaGenerator {
        public static float Next() {
            var validDouble = Random.insideUnitCircle;
            float s = validDouble.sqrMagnitude;
            if (s == 0) {
                return Next();
            }
            return validDouble.x * Mathf.Sqrt((-2 * Mathf.Log(s)) / s);
        }

        public static Vector3 NextVector3() {
            return new Vector3(Next(), Next(), Next());
        }
    }
}