using ManusVR.Core.Apollo;
using PFVR.OurPhysics;
using PFVR.Player;
using System.Collections;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject ballPrefab = default;

        [SerializeField]
        private GameObject anchorPrefab = default;

        [SerializeField]
        private GameObject explosionPrefab = default;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleInterval = 100;

        [SerializeField]
        private AnimationCurve rumbleForceOverDistance = default;

        [SerializeField]
        private AnimationCurve rumbleForceOverSize = default;

        private Coroutine rumbleRoutine;

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
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand));
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            ball?.ReleaseFrom(anchor, player.rigidbody.velocity);
            ball = null;
            if (rumbleRoutine != null) {
                StopCoroutine(rumbleRoutine);
            }
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            chargeTime = Mathf.Clamp(chargeTime + Time.fixedDeltaTime, 0, maximumChargeTime);
            if (ball != null) {
                ball.transform.Translate(player.deltaMovement);
            }
        }
        private IEnumerator CreateRumbleRoutine(PlayerHandBehaviour hand) {
            while (true) {
                if (ball != null) {
                    var distance = (anchor.transform.position - ball.transform.position).magnitude;
                    Debug.Log(distance);
                    Apollo.rumble(hand.laterality, rumbleInterval, (ushort)(rumbleForceOverDistance.Evaluate(distance) * rumbleForceOverSize.Evaluate(ball.size) * ushort.MaxValue));
                }
                yield return new WaitForSeconds(rumbleInterval / 1000f);
            }
        }
    }
}