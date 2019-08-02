using ManusVR.Core.Apollo;
using PFVR.OurPhysics;
using PFVR.Player;
using PFVR.Player.Gestures;
using Slothsoft.UnityExtensions;
using System.Collections;
using UnityEngine;

namespace PFVR.Spells.EnergyWave {
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject wavePrefab = default;

        [SerializeField, Range(0, 100)]
        private float breakSpeed = 0;

        [SerializeField, Range(0, 1000)]
        private float handLaunchMultiplier = 1;
        [SerializeField, Range(0, 1000)]
        private float directionLaunchMultiplier = 1;


        [SerializeField, Range(1, 1000)]
        private ushort rumbleInterval = 100;

        [SerializeField]
        private AnimationCurve rumbleForceOverSize = default;

        private Coroutine rumbleRoutine;

        private Wave wave;

        [SerializeField]
        private float maximumChargeTime = 1f;
        private float currentChargeTime = 0;

        [SerializeField, Range(0, 100)]
        private float followSpeed = 1f;

        private float chargeTime {
            get {
                return currentChargeTime;
            }
            set {
                currentChargeTime = Mathf.Clamp(value, 0, maximumChargeTime);
                if (wave != null) {
                    wave.size = currentChargeTime / maximumChargeTime;
                }
            }
        }

        private Transform leftChargeCenter;
        private Transform rightChargeCenter;
        private Vector3 chargeCenter => (leftChargeCenter.position + rightChargeCenter.position) / 2;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (hand.laterality == GloveLaterality.GLOVE_LEFT) {
                return;
            }

            leftChargeCenter = player.leftHand.gatheringCenter;
            rightChargeCenter = player.rightHand.gatheringCenter;

            wave = Instantiate(wavePrefab, player.transform).GetComponent<Wave>();
            wave.GetComponentsInChildren<TrailRenderer>()
                .ForAll(renderer => renderer.enabled = false);
            wave.transform.position = chargeCenter;
            wave.explodable = false;
            chargeTime = 0;
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine());
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (hand.laterality == GloveLaterality.GLOVE_LEFT) {
                return;
            }

            if (wave != null) {
                wave.GetComponentsInChildren<TrailRenderer>()
                    .ForAll(renderer => renderer.enabled = true);
                wave.transform.parent = wave.transform.parent.parent;
                wave.explodable = true;
                wave.body.velocity = handLaunchMultiplier * (player.leftHand.velocity + player.rightHand.velocity) + directionLaunchMultiplier * (leftChargeCenter.position - player.leftHand.wrist.position + rightChargeCenter.position - player.rightHand.wrist.position);
                wave = null;
            }
            if (rumbleRoutine != null) {
                StopCoroutine(rumbleRoutine);
            }
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (hand.laterality == GloveLaterality.GLOVE_LEFT) {
                return;
            }

            chargeTime += Time.deltaTime;
            if (wave != null) {
                wave.transform.position = Vector3.Lerp(wave.transform.position, chargeCenter, followSpeed * Time.deltaTime);
            }
            player.motor.Break(breakSpeed * Time.deltaTime);
        }

        private IEnumerator CreateRumbleRoutine() {
            while (true) {
                if (wave != null) {
                    ManusConnector.Rumble(GloveLaterality.GLOVE_LEFT, rumbleInterval, rumbleForceOverSize.Evaluate(wave.size));
                    ManusConnector.Rumble(GloveLaterality.GLOVE_RIGHT, rumbleInterval, rumbleForceOverSize.Evaluate(wave.size));
                }
                yield return new WaitForSeconds(rumbleInterval / 1000f);
            }
        }
    }
}