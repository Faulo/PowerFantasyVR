﻿using ManusVR.Core.Apollo;
using PFVR.OurPhysics;
using PFVR.Player;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace PFVR.Spells {
    [RequireComponent(typeof(AbstractSpell))]
    public class LaserRay : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject rayPrefab = default;

        [SerializeField, Range(1, 1000)]
        private ushort rayInterval = 1000;

        [SerializeField, Range(1, 100000)]
        private float rayRange = 1000;

        [SerializeField, Range(0, 10)]
        private float rayLifetime = 1;

        [SerializeField, Range(0, 1000)]
        private float rayForce = 100;

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
                var ray = Instantiate(rayPrefab).GetComponent<LineRenderer>();
                var start = hand.indexFinger.position;
                var direction = hand.indexFinger.forward;
                ray.SetPosition(0, start);
                ray.SetPosition(1, start + direction * rayRange);
                var hits = Physics.RaycastAll(start, direction, rayRange)
                    .Select(hit => hit.collider);
                hits.SelectMany(collider => collider.GetComponents<Rigidbody>())
                    .ForAll(body => body.AddForce(direction * rayForce));
                if (destruction != null) {
                    hits.SelectMany(collider => collider.GetComponentsInChildren<Renderer>())
                        .ForAll(renderer => renderer.material = destruction);
                }
                Destroy(ray.gameObject, rayLifetime);
                Apollo.rumble(hand.laterality, rumbleDuration, (ushort)(rumbleForce * ushort.MaxValue));
                yield return new WaitForSeconds(rayInterval / 1000f);
            }
        }
    }
}