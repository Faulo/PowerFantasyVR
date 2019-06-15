using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    public interface IRay {
        void Fire(Vector3 position, Vector3 direction, float range, float force, float lifetime);
    }
}
