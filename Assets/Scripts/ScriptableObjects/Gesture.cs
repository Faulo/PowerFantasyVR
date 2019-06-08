using ManusVR.Core.Apollo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Gesture", menuName = "Gameplay/Gesture", order = 1)]
    public class Gesture : ScriptableObject {
        public GameObject spellPrefab;
    }
}