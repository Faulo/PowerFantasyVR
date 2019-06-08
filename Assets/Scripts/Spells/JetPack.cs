using ManusVR.Core.Apollo;
using PFVR.Player;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells {
    [RequireComponent(typeof(AbstractSpell))]
    public class JetPack : MonoBehaviour, ISpellState {
        [SerializeField]
        private float propulsionForce = 1000;

        [SerializeField]
        private float antiGravityForce = 100;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleInterval = 100;

        [SerializeField, Range(0f, 1f)]
        private float rumbleForce = 0.5f;

        private Coroutine rumbleRoutine;

        [SerializeField]
        private AnimationCurve propulsionOverTime = default;
        private float runTime;

        [SerializeField]
        private GameObject enginePrefab = default;
        private GameObject engine;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (engine == null) {
                engine = Instantiate(enginePrefab, hand.wrist);
            }
            engine?.SetActive(true);
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand.laterality));
            runTime = 0;
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            engine?.SetActive(false);
            StopCoroutine(rumbleRoutine);
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            runTime += Time.deltaTime;
            player.rigidbody.AddForce(hand.wrist.up * propulsionForce * Time.deltaTime * propulsionOverTime.Evaluate(runTime));
            player.rigidbody.AddForce(player.transform.up * antiGravityForce * Time.deltaTime);
        }
        private IEnumerator CreateRumbleRoutine(GloveLaterality side) {
            while (true) {
                Apollo.rumble(side, rumbleInterval, (ushort) (rumbleForce * ushort.MaxValue));
                yield return new WaitForSeconds(rumbleInterval / 1000f);
            }
        }
    }
}