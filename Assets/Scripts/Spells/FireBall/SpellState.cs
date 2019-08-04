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


        [SerializeField, Range(1, 1000)]
        private ushort rumbleInterval = 100;

        [SerializeField]
        private AnimationCurve rumbleForceOverDistance = default;

        [SerializeField]
        private AnimationCurve rumbleForceOverSize = default;

        private Coroutine rumbleRoutine;

        private SpringJoint anchor;

        private Ball ball;

        [SerializeField]
        private float maximumChargeTime = 1f;
        private float currentChargeTime = 0;

        private float chargeTime {
            get {
                return currentChargeTime;
            }
            set {
                currentChargeTime = Mathf.Clamp(value, 0, maximumChargeTime);
                if (ball != null) {
                    ball.size = currentChargeTime / maximumChargeTime;
                }
            }
        }

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (anchor == null) {
                anchor = Instantiate(anchorPrefab, hand.wrist).GetComponent<SpringJoint>();
            }
            ball = Instantiate(ballPrefab, player.transform).GetComponent<Ball>();
            ball.ConnectTo(anchor);
            chargeTime = 0;
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand));
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (ball != null) {
                ball.body.velocity -= player.motor.velocity;
                ball.ReleaseFrom(anchor);
                ball = null;
            }
            if (rumbleRoutine != null) {
                StopCoroutine(rumbleRoutine);
            }
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            chargeTime += Time.deltaTime;
            if (ball != null && anchor != null) {
                var distance = anchor.transform.position - ball.transform.position;
                ball.body.AddForce(distance, ForceMode.VelocityChange);
                //anchor.damper = Mathf.Clamp(1 / distance.magnitude, 1, 1000);
                anchor.spring = Mathf.Clamp(player.motor.speed, 1, 1000);
            }
            //player.motor.Break(breakSpeed);
        }
        private IEnumerator CreateRumbleRoutine(PlayerHandBehaviour hand) {
            while (true) {
                if (ball != null) {
                    var distance = (anchor.transform.position - ball.transform.position).magnitude;
                    ManusConnector.Rumble(hand.laterality, rumbleInterval, rumbleForceOverDistance.Evaluate(distance) * rumbleForceOverSize.Evaluate(ball.size));
                }
                yield return new WaitForSeconds(rumbleInterval / 1000f);
            }
        }
    }
}