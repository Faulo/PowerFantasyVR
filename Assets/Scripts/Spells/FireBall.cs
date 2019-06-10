using System.Collections;
using System.Collections.Generic;
using PFVR.Player;
using UnityEngine;

namespace PFVR.Spells {
    public class FireBall : MonoBehaviour, ISpellState {
        [SerializeField]
        private GameObject ballPrefab;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            throw new System.NotImplementedException();
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            throw new System.NotImplementedException();
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
            throw new System.NotImplementedException();
        }
        void Start() {

        }
    }

}