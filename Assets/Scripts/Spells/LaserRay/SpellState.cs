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

        [Space]
        [SerializeField, Range(1, 1000)]
        private ushort rumbleDuration = 100;
        [SerializeField, Range(0f, 1f)]
        private float rumbleForceIdle = 0.5f;
        [SerializeField, Range(0f, 1f)]
        private float rumbleForceCutting = 1.0f;

        private BasicRay ray;
        private Coroutine rayRoutine;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            ray = Instantiate(rayPrefab, hand.indexFinger).GetComponent<BasicRay>();
            rayRoutine = StartCoroutine(CreateRayRoutine(hand));
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (ray != null) {
                Destroy(ray.gameObject);
                ray = null;
            }
            if (rayRoutine != null) {
                StopCoroutine(rayRoutine);
                rayRoutine = null;
            }
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        private IEnumerator CreateRayRoutine(PlayerHandBehaviour hand) {
            while (ray) {
                ManusConnector.Rumble(hand.laterality, rumbleDuration, ray.isCutting ? rumbleForceCutting : rumbleForceIdle);
                yield return new WaitForSeconds(rumbleDuration / 1000f);
            }
        }
    }
}