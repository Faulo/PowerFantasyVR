using PFVR.Tracking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Gesture Set", menuName = "Gameplay/Gesture Set", order = 1)]
    public class GestureSet : ScriptableObject {
        [SerializeField]
        private Gesture[] gestures;

        public Gesture this[string index] {
            get {
                return gestures
                    .Where(gesture => gesture.id == index)
                    .FirstOrDefault();
            }
        }
    }
}