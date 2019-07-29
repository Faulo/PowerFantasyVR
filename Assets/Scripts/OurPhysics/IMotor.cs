using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    public interface IMotor {
        Vector3 position { get; }
        Vector3 velocity { get; }
        float speed { get; }

        void Break(float breakFactor);
        void LerpVelocity(Vector3 targetVelocity, float factor);
        void AddVelocity(Vector3 addedVelocity);
    }
}