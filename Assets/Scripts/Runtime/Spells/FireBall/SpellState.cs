using System.Collections;
using PFVR.OurPhysics;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.FireBall {
    public sealed class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        GameObject ballPrefab = default;

        [SerializeField]
        GameObject anchorPrefab = default;

        [Space]
        [SerializeField, Range(1, 1000)]
        ushort rumbleInterval = 100;
        [SerializeField]
        AnimationCurve rumbleForceOverSize = default;

        Coroutine rumbleRoutine;

        [Space]
        [SerializeField, Range(0, 100)]
        float launchVelocityMultiplier = 1;
        [SerializeField, Range(1, 10)]
        float maximumChargeTime = 1f;
        float currentChargeTime = 0;

        Anchor anchor;
        Ball ball;

        float chargeTime {
            get {
                return currentChargeTime;
            }
            set {
                currentChargeTime = Mathf.Clamp(value, 0, maximumChargeTime);
                if (ball != null) {
                    ball.scaling = currentChargeTime / maximumChargeTime;
                }
            }
        }

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (anchor == null) {
                anchor = Instantiate(anchorPrefab, hand.wrist).GetComponent<Anchor>();
            }

            ball = Instantiate(ballPrefab, player.transform).GetComponent<Ball>();
            anchor.ConnectTo(ball);
            chargeTime = 0;
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand));
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (ball != null) {
                anchor.ReleaseFrom(ball);
                ball.rigidbody.velocity += hand.velocity;
                ball.rigidbody.velocity *= launchVelocityMultiplier;
                ball.rigidbody.gameObject.AddComponent<KinematicRigidbody>();
                ball = null;
            }

            if (rumbleRoutine != null) {
                StopCoroutine(rumbleRoutine);
            }
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            chargeTime += Time.deltaTime;
        }
        IEnumerator CreateRumbleRoutine(PlayerHandBehaviour hand) {
            while (true) {
                if (ball != null) {
                    ManusConnector.Rumble(hand.laterality, rumbleInterval, rumbleForceOverSize.Evaluate(ball.scaling));
                }

                yield return new WaitForSeconds(rumbleInterval / 1000f);
            }
        }
    }
}