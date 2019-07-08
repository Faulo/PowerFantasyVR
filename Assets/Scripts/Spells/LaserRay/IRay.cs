using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    public interface IRay {
        void UpdateRay(Vector3 position, Vector3 direction, float range, float force);
        void Stop();
    }
}
