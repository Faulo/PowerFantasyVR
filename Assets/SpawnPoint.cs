using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Player {
    public class SpawnPoint : MonoBehaviour {
        public enum PlayerType {
            Basic,
            VR
        }
        [SerializeField]
        private PlayerType playerType = default;
        [SerializeField]
        private GameObject basicPlayerPrefab = default;
        [SerializeField]
        private GameObject vrPlayerPrefab = default;

        private GameObject playerPrefab {
            get {
                switch (playerType) {
                    case PlayerType.Basic:
                        return basicPlayerPrefab;
                    case PlayerType.VR:
                        return vrPlayerPrefab;
                }
                throw new System.Exception("???" + playerType);
            }
        }

        void Awake() {
            Instantiate(playerPrefab, transform);
        }
    }
}