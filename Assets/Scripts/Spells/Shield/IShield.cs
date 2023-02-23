using System;
using UnityEngine;

namespace PFVR.Spells.Shield {
    public interface IShield {
        event Action<GameObject> onCollision;
        void Explode();
    }
}
