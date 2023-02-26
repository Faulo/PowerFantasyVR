using System.Collections;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.LaserBolt {
    [RequireComponent(typeof(AbstractSpell))]
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        GameObject boltPrefab = default;

        [SerializeField, Range(1, 1000)]
        float boltVelocity = 100;

        [SerializeField, Range(1, 1000)]
        float boltInterval = 1000;

        [SerializeField, Range(0, 60)]
        float boltLifetime = 10;

        [SerializeField, Range(1, 1000)]
        ushort rumbleDuration = 100;

        [SerializeField, Range(0f, 1f)]
        float rumbleForce = 0.5f;

        Coroutine boltRoutine;

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
        IEnumerator CreateBoltRoutine(PlayerHandBehaviour hand) {
            while (true) {
                var bolt = Instantiate(boltPrefab, hand.indexFinger.position, hand.indexFinger.rotation).GetComponent<Bolt>();
                bolt.velocity = bolt.transform.forward * boltVelocity;
                Destroy(bolt.gameObject, boltLifetime);
                ManusConnector.Rumble(hand.laterality, rumbleDuration, rumbleForce);
                yield return new WaitForSeconds(boltInterval / 1000f);
            }
        }
    }
}