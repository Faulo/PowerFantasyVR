using ManusVR.Core.Apollo;
using PFVR.OurPhysics;
using System.Collections;
using UnityEngine;

namespace PFVR.Spells {
    [RequireComponent(typeof(AbstractSpell))]
    public class LaserBolt : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject boltPrefab = default;

        [SerializeField]
        private float boltVelocity = 1000;

        [SerializeField, Range(1, 1000)]
        private float boltInterval = 1000;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleDuration = 100;

        [SerializeField, Range(0f, 1f)]
        private float rumbleForce = 0.5f;

        private Coroutine boltRoutine;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            boltRoutine = StartCoroutine(CreateBoltRoutine(hand));
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            StopCoroutine(boltRoutine);
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
        private IEnumerator CreateBoltRoutine(PlayerHandBehaviour hand) {
            while (true) {
                var bolt = Instantiate(boltPrefab, hand.indexFinger.position, hand.indexFinger.rotation);
                bolt.GetComponent<KinematicRigidbody>().velocity = hand.owner.rigidbody.velocity;
                bolt.GetComponent<KinematicRigidbody>().velocity += bolt.transform.forward * boltVelocity;
                Apollo.rumble(hand.laterality, rumbleDuration, (ushort)(rumbleForce * ushort.MaxValue));
                yield return new WaitForSeconds(boltInterval / 1000f);
            }
        }
    }
}