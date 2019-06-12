using System.Collections;
using System.Collections.Generic;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells.Shield {
    public class SpellState : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject hamsterBallPrefab = default;

        private HamsterBall hamsterBall;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            hamsterBall = Instantiate(hamsterBallPrefab, player.torso).GetComponent<HamsterBall>();
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (hamsterBall != null) {
                hamsterBall.Explode();
            }
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
    }
}