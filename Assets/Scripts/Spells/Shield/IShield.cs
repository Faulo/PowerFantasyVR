using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.Shield {
    public interface IShield {
        event Action<GameObject> onCollision;
        void Explode();
    }
}
