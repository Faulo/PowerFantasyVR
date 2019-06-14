using ManusVR.Core.Apollo;
using PFVR.OurPhysics;
using PFVR.Player;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells.LaserRay {
    [RequireComponent(typeof(AbstractSpell))]
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject rayPrefab = default;

        [SerializeField, Range(1, 100)]
        private float rayForce = 50;

        [SerializeField, Range(1, 1000)]
        private ushort rayInterval = 1000;

        [SerializeField, Range(1, 100000)]
        private float rayRange = 1000;

        [SerializeField, Range(0, 10)]
        private float rayLifetime = 1;

        [SerializeField]
        private Material destruction = default;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleDuration = 100;

        [SerializeField, Range(0f, 1f)]
        private float rumbleForce = 0.5f;

        private Coroutine rayRoutine;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (rumbleDuration > rayInterval) {
                rumbleDuration = rayInterval;
            }
            rayRoutine = StartCoroutine(CreateRayRoutine(hand));
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (rayRoutine != null) {
                StopCoroutine(rayRoutine);
            }
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        private IEnumerator CreateRayRoutine(PlayerHandBehaviour hand) {
            while (true) {
                var ray = Instantiate(rayPrefab).GetComponent<IRay>();
                ray.Fire(hand.indexFinger.position, hand.indexFinger.forward, rayRange, rayForce, rayLifetime);

                Apollo.rumble(hand.laterality, rumbleDuration, (ushort)(rumbleForce * ushort.MaxValue));
                yield return new WaitForSeconds(rayInterval / 1000f);
            }
        }
    }
}