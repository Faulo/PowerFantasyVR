using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.TimeStop {
    [RequireComponent(typeof(AbstractSpell))]
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private float defaultTimeScale = 1;

        [SerializeField]
        private float stoppedTimeScale = 0;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            Time.timeScale = stoppedTimeScale;
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            Time.timeScale = defaultTimeScale;
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        void Start() {
            if (stoppedTimeScale < Mathf.Epsilon) {
                stoppedTimeScale = Mathf.Epsilon;
            }
        }
    }
}