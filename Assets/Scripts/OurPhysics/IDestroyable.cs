using UnityEngine;


namespace PFVR.OurPhysics {
    public interface IDestroyable {
        float currentHP { get; set; }
        bool isAlive { get; }
        Rigidbody rigidbody { get; }
        Vector3 position { get; }
    }
}