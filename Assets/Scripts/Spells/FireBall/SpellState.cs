using PFVR.OurPhysics;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject ballPrefab = default;

        [SerializeField]
        private GameObject anchorPrefab = default;

        private Joint anchor;

        private Ball ball;

        [SerializeField]
        private float maximumChargeTime = 1f;
        private float chargeTime;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (anchor == null) {
                anchor = Instantiate(anchorPrefab, hand.wrist).GetComponent<Joint>();
            }
            ball = Instantiate(ballPrefab).GetComponent<Ball>();
            ball.ConnectTo(anchor);
            chargeTime = 0;
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            ball.ReleaseFrom(anchor, player.rigidbody.velocity);
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            chargeTime += Time.fixedDeltaTime;
            ball.transform.Translate(player.deltaMovement);
            ball.size = chargeTime / maximumChargeTime;
        }
    }
}