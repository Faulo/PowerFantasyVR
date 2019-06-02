using UnityEngine;

namespace PFVR.Gestures {
    public class Nothing : MonoBehaviour, IGestureState {
        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(true);
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(false);
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
    }
}