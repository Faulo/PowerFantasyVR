﻿using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.Shield {
    public sealed class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        GameObject shieldPrefab = default;

        [SerializeField, Range(0f, 100f)]
        float breakSpeed = 0f;

        [SerializeField, Range(0, 10000)]
        ushort rumbleDuration = 1000;
        [SerializeField, Range(0f, 1f)]
        float rumbleForce = 1f;

        IShield shield;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            shield = Instantiate(shieldPrefab, hand.middleFinger).GetComponent<IShield>();
            shield.onCollision += (go) => {
                ManusConnector.Rumble(hand.laterality, rumbleDuration, rumbleForce);
            };
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            shield?.Explode();
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            player.motor.Break(breakSpeed * Time.deltaTime);
        }
    }
}