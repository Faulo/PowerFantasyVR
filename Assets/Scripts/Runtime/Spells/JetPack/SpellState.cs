using System.Collections;
using ManusVR.Core.Apollo;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.JetPack {
    [RequireComponent(typeof(AbstractSpell))]
    public sealed class SpellState : MonoBehaviour, ISpellState {
        [SerializeField, Range(0, 100)]
        float propulsionForce = 10;
        [SerializeField]
        AnimationCurve propulsionOverTime = default;
        [SerializeField]
        AnimationCurve propulsionOverAltitude = default;
        [SerializeField, Range(0f, 1f)]
        float propulsionRumble = 0.5f;
        [SerializeField, Range(0f, 1000f)]
        float maximumAltitude = 100f;
        float altitude => Physics.Raycast(transform.position, Vector3.down, out var hit, maximumAltitude, LayerMask.GetMask("Ground"))
                    ? hit.distance
                    : maximumAltitude;
        float normalizedAltitude => altitude / maximumAltitude;

        [Space]
        [SerializeField, Range(0, 1000)]
        float boostForce = 100;
        [SerializeField, Range(0, 10)]
        float boostDuration = 1;
        [SerializeField, Range(0f, 1f)]
        float boostRumble = 1f;
        float boostTime = 0;

        [Space]
        [SerializeField, Range(0, 100)]
        float turnSpeed = 1f;
        [SerializeField, Range(-1, 0)]
        float gravityNegation = 0;

        [Space]
        [SerializeField, Range(1, 1000)]
        ushort rumbleInterval = 100;
        Coroutine rumbleRoutine;

        float runTime {
            get => runTimeCache;
            set {
                runTimeCache = Mathf.Clamp(value, 0, 1);
                if (engine != null) {
                    engine.propulsion = propulsionOverTime.Evaluate(runTimeCache);
                }
            }
        }
        float runTimeCache;

        [SerializeField]
        GameObject enginePrefab = default;
        Engine engine;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (engine == null) {
                engine = Instantiate(enginePrefab, hand.wrist).GetComponent<Engine>();
            }
            engine.isTurnedOn = true;
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand.laterality));
            runTime = 0;
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            engine.isTurnedOn = false;
            if (rumbleRoutine != null) {
                StopCoroutine(rumbleRoutine);
            }
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            runTime += Time.deltaTime;
            boostTime -= Time.deltaTime;
            if (hand.isShaking) {
                boostTime = boostDuration;
            }
            engine.isBoosting = boostTime > 0;

            var turn = player.motor.velocity + hand.wrist.up;
            if (turn.magnitude < player.motor.speed) {
                player.motor.LerpVelocity(turn, turnSpeed * Time.deltaTime);
            }
            var direction = new Vector3(hand.wrist.up.x, hand.wrist.up.y * propulsionOverAltitude.Evaluate(normalizedAltitude), hand.wrist.up.z);
            player.motor.AddVelocity(direction * (engine.isBoosting ? boostForce : propulsionForce) * Time.deltaTime * engine.propulsion);
            player.motor.AddVelocity(Physics.gravity * gravityNegation * Time.deltaTime);
        }
        IEnumerator CreateRumbleRoutine(GloveLaterality side) {
            while (true) {
                ManusConnector.Rumble(side, rumbleInterval, engine.propulsion * (engine.isBoosting ? boostRumble : propulsionRumble));
                yield return new WaitForSeconds(rumbleInterval / 1000f);
            }
        }
    }
}