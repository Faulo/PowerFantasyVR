using PFVR;
using PFVR.Player;
using PFVR.Spells;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR;

namespace PFVR.Spells {
    public class AbstractSpell : MonoBehaviour, ISpellState {
        [SerializeField]
        [Tooltip("The material that is applied to the HTC Vive tracker model while this spell is active.")]
        private Material trackerMaterial = default;

        public void OnEnter(PlayerBehaviour player, PlayerHandBehaviour hand) {
            if (trackerMaterial != default) {
                hand.tracker.GetComponentsInChildren<MeshRenderer>()
                    .Reverse()
                    .Take(1)
                    .ForAll(renderer => renderer.material = trackerMaterial);
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