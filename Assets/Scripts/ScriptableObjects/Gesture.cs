using PFVR.Spells;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Gesture", menuName = "Gameplay/Gesture", order = 1)]
    public class Gesture : ScriptableObject {
        public GameObject spellPrefab = default;
        public Sprite icon = default;
        public Color spellColor => spellPrefab.GetComponent<AbstractSpell>().glowColor;

        void OnValidate() {
            if (spellPrefab != default && spellPrefab.GetComponent<ISpellState>() == null) {
                Debug.LogError("Spell Prefab must contain at least 1 ISpellState Component!");
                spellPrefab = default;
            }
        }
    }
}