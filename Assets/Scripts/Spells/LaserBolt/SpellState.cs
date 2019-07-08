using ManusVR.Core.Apollo;
using PFVR.OurPhysics;
using PFVR.Player;
using System.Collections;
using UnityEngine;

namespace PFVR.Spells.LaserBolt {
    [RequireComponent(typeof(AbstractSpell))]
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject boltPrefab = default;

        [SerializeField, Range(1, 100)]
        private float boltVelocity = 1000;

        [SerializeField, Range(1, 1000)]
        private float boltInterval = 1000;

        [SerializeField, Range(0, 60)]
        private float boltLifetime = 10;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleDuration = 100;

        [SerializeField, Range(0f, 1f)]
        private float rumbleForce = 0.5f;

        private Coroutine boltRoutine;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            boltRoutine = StartCoroutine(CreateBoltRoutine(hand));
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (boltRoutine != null) {
                StopCoroutine(boltRoutine);
            }
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        private IEnumerator CreateBoltRoutine(PlayerHandBehaviour hand) {
            while (true) {
                var bolt = Instantiate(boltPrefab, hand.indexFinger.position, hand.indexFinger.rotation).GetComponent<Bolt>();
                bolt.velocity = hand.owner.rigidbody.velocity + bolt.transform.forward * boltVelocity;
                Destroy(bolt.gameObject, boltLifetime);
                Apollo.rumble(hand.laterality, rumbleDuration, (ushort)(rumbleForce * ushort.MaxValue));
                yield return new WaitForSeconds(boltInterval / 1000f);
            }
        }
    }
}