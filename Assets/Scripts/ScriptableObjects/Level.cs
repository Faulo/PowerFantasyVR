using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Level", menuName = "Gameplay/Level", order = 50)]
    public class Level : ScriptableObject {
        public SceneAsset scene = default;
    }
}