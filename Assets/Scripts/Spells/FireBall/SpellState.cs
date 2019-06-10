using PFVR.OurPhysics;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject ballPrefab = default;

        [SerializeField]
        private GameObject anchorPrefab = default;

        [SerializeField]
        private GameObject explosionPrefab = default;

        private Joint anchor;

        private Ball ball;

        [SerializeField]
        private float maximumChargeTime = 1f;
        private float currentChargeTime = 0;
        private float chargeTime {
            get {
                return currentChargeTime;
            }
            set {
                currentChargeTime = Mathf.Clamp(value, 0, 1);
                if (ball != null) {
                    ball.size = currentChargeTime / maximumChargeTime;
                }
            }
        }

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (anchor == null) {
                anchor = Instantiate(anchorPrefab, hand.wrist).GetComponent<Joint>();
            }
            ball = Instantiate(ballPrefab).GetComponent<Ball>();
            ball.ConnectTo(anchor);
            ball.onCollisionEnter += (ball, collision) => {
                var explosion = Instantiate(explosionPrefab, ball.transform.position, ball.transform.rotation).GetComponent<Explosion>();
                explosion.size = ball.size;
                Destroy(ball.gameObject);
            };
            chargeTime = 0;
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            ball?.ReleaseFrom(anchor, player.rigidbody.velocity);
            ball = null;
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            chargeTime += Time.fixedDeltaTime;
            ball?.transform.Translate(player.deltaMovement);
        }
    }
}