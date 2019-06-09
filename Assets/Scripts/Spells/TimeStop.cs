using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells {
    [RequireComponent(typeof(AbstractSpell))]
    public class TimeStop : MonoBehaviour, ISpellState {
        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            player.rigidbody.velocity *= 0.1f;
        }
    }
}