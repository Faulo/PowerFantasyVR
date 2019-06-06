using PFVR;
using PFVR.Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace PFVR.Spells {
    public class AbstractSpell : MonoBehaviour, ISpellState {
        [SerializeField]
        [Tooltip("The material that is applied to the HTC Vive tracker model while this spell is active.")]
        private Material trackerMaterial = default;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (trackerMaterial != default) {
                hand.tracker.GetComponentInChildren<MeshRenderer>().material = trackerMaterial;
            }
            gameObject.SetActive(true);
        }

        public void OnExit(PlayerBehaviour player, PlayerHandBehaviour hand) {
            gameObject.SetActive(false);
        }

        public void OnUpdate(PlayerBehaviour player, PlayerHandBehaviour hand) {
        }
    }
}