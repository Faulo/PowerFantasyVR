using ManusVR.Core.Apollo;
using PFVR.Player;
using System.Collections;
using UnityEngine;

namespace PFVR.Spells.JetPack {
    [RequireComponent(typeof(AbstractSpell))]
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        [Range(0, 100)]
        private float propulsionForce = 10;

        [SerializeField]
        [Range(-1, 0)]
        private float gravityNegation = 0;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleInterval = 100;

        [SerializeField, Range(0f, 1f)]
        private float rumbleForce = 0.5f;

        private Coroutine rumbleRoutine;

        [SerializeField]
        private AnimationCurve propulsionOverTime = default;
        private float runTime {
            get => runTimeCache;
            set {
                runTimeCache = Mathf.Clamp(value, 0, 1);
                if (engine != null) {
                    engine.propulsion = propulsionOverTime.Evaluate(runTimeCache);
                }
            }
        }
        private float runTimeCache;

        [SerializeField]
        private GameObject enginePrefab = default;
        private Engine engine;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (engine == null) {
                engine = Instantiate(enginePrefab, hand.wrist).GetComponent<Engine>();
            }
            engine.TurnOn();
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand.laterality));
            runTime = 0;
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            engine.TurnOff();
            if (rumbleRoutine != null) {
                StopCoroutine(rumbleRoutine);
            }
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            runTime += Time.fixedDeltaTime;
            player.rigidbody.AddForce(hand.wrist.up * propulsionForce * Time.fixedDeltaTime * engine.propulsion, ForceMode.VelocityChange);
            player.rigidbody.AddForce(Physics.gravity * gravityNegation * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        private IEnumerator CreateRumbleRoutine(GloveLaterality side) {
            while (true) {
                Apollo.rumble(side, rumbleInterval, (ushort)(engine.propulsion * rumbleForce * ushort.MaxValue));
                yield return new WaitForSeconds(rumbleInterval / 1000f);
            }
        }
    }
}