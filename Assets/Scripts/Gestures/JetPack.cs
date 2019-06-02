using ManusVR.Core.Apollo;
using System.Collections;
using UnityEngine;

namespace PFVR.Gestures {
    public class JetPack : MonoBehaviour, IGestureState {
        [SerializeField]
        private float propulsionForce = 1000;

        [SerializeField]
        private float antiGravityForce = 100;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleInterval = 100;

        [SerializeField, Range(0f, 1f)]
        private float rumbleForce = 0.5f;

        private GameObject particles;
        private Coroutine rumbleRoutine;

        private void Awake() {
            particles = transform.Find("Particles").gameObject;
        }

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(true);
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand.laterality));
        }
        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(false);
            StopCoroutine(rumbleRoutine);
        }
        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            player.rigidbody.AddForce(hand.transform.forward * propulsionForce * Time.deltaTime);
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