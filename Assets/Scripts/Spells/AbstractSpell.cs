using PFVR;
using PFVR.Player;
using PFVR.Spells;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR;

namespace PFVR.Spells {
    public class AbstractSpell : MonoBehaviour, ISpellState {
        [SerializeField, ColorUsage(true, true)]
        [Tooltip("The color the infinity stone will be glowing in.")]
        public Color glowColor = default;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            var renderer = hand.infinityStone.GetComponentInChildren<MeshRenderer>();
            if (glowColor == default) {
                renderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
                renderer.material.DisableKeyword("_EMISSION");
            } else {
                renderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                renderer.material.EnableKeyword("_EMISSION");
                renderer.material.SetColor("_EmissionColor", glowColor);
            }
            gameObject.SetActive(true);
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(false);
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
    }
}