using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.TimeStop {
    [RequireComponent(typeof(AbstractSpell))]
    public class SpellState : MonoBehaviour, ISpellState {
        float defaultTimeScale = 1;

        [SerializeField, Range(0, 1)]
        float stoppedTimeScale = 0;

        [SerializeField, Range(0, 100)]
        float breakSpeed = 1;

        [SerializeField]
        GameObject postProcessingPrefab = default;
        GameObject postProcessing;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            Time.timeScale = stoppedTimeScale;
            if (postProcessingPrefab) {
                postProcessing = Instantiate(postProcessingPrefab, player.transform);
            }
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            Time.timeScale = defaultTimeScale;
            if (postProcessing) {
                Destroy(postProcessing);
            }
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            player.motor.Break(breakSpeed * Time.deltaTime);
        }
        void Start() {
            if (stoppedTimeScale < Mathf.Epsilon) {
                stoppedTimeScale = Mathf.Epsilon;
            }
        }
    }
}