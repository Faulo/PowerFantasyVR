using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells {
    public class AbstractSpell : MonoBehaviour, ISpellState {
        [SerializeField, ColorUsage(true, true)]
        [Tooltip("The color the infinity stone will be glowing in.")]
        public Color glowColor = default;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            hand.infinityStone.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", glowColor);
            gameObject.SetActive(true);
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(false);
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
    }
}