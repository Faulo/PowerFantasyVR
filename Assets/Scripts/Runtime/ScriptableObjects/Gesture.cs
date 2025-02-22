﻿using PFVR.Spells;
using UnityEngine;

namespace PFVR.ScriptableObjects {
    /// <summary>
    /// A single gesture. Its name will be used as ID elsewhere.
    /// </summary>
    [CreateAssetMenu(fileName = "New Gesture", menuName = "Gameplay/Gesture", order = 1)]
    public sealed class Gesture : ScriptableObject {
        public GameObject spellPrefab = default;
        public Sprite icon = default;
        public Color spellColor => spellPrefab.GetComponent<AbstractSpell>().glowColor;
        public bool isComplex = false;

        void OnValidate() {
            if (spellPrefab != default && spellPrefab.GetComponent<ISpellState>() == null) {
                Debug.LogError("Spell Prefab must contain at least 1 ISpellState Component!");
                spellPrefab = default;
            }
        }
    }
}