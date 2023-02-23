using UnityEngine;

namespace PFVR.ScriptableObjects {
    /// <summary>
    /// A storable set of level information. Its name will be used in the level selection.
    /// </summary>
    [CreateAssetMenu(fileName = "New Level", menuName = "Gameplay/Level", order = 50)]
    public class Level : ScriptableObject {
        public SceneReference scene = default;
    }
}