using ManusVR.Core.Apollo;
using PFVR;
using PFVR.Gestures;
using PFVR.Tracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Gestures {
    public class JetPack : MonoBehaviour, IGestureState {
        [SerializeField]
        private float propulsionForce = 1000;

        [SerializeField]
        private float antiGravityForce = 100;

        [SerializeField, Range(1, 1000)]
        private ushort rumbleDuration = 100;

        [SerializeField, Range(0f, 1f)]
        private float rumbleForce = 0.5f;

        private GameObject particles;
        private Coroutine rumbleRoutine;

        private void Awake() {
            particles = transform.Find("Particles").gameObject;
        }

        public void OnEnter(PlayerBehaviour player, Hand hand) {
            rumbleRoutine = StartCoroutine(CreateRumbleRoutine(hand.side));
            player.rigidbody.useGravity = false;
            particles.SetActive(true);
        }
        public void OnExit(PlayerBehaviour player, Hand hand) {
            StopCoroutine(rumbleRoutine);
            player.rigidbody.useGravity = true;
            particles.SetActive(false);
        }
        public void OnUpdate(PlayerBehaviour player, Hand hand) {
            player.rigidbody.AddForce(hand.tracker.transform.forward * propulsionForce * Time.deltaTime);
            player.rigidbody.AddForce(Vector3.up * antiGravityForce * Time.deltaTime);
        }
        private IEnumerator CreateRumbleRoutine(GloveLaterality side) {
            while (true) {
                Apollo.rumble(side, rumbleDuration, (ushort) (rumbleForce * ushort.MaxValue));
                yield return new WaitForSeconds(rumbleDuration / 1000f);
            }
        }
    }
}