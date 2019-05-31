using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Gesture Set", menuName = "Gameplay/Gesture Set", order = 1)]
    public class GestureSet : ScriptableObject {
        [SerializeField]
        private Gesture[] gestures;
    }
}