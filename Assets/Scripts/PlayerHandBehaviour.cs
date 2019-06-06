using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using PFVR.ScriptableObjects;
using PFVR.Spells;
using Slothsoft.UnityExtensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR {
    public class PlayerHandBehaviour : MonoBehaviour {
        public PlayerBehaviour owner { get; private set; }
        public GloveLaterality laterality { get; private set; }
        public Transform tracker {
            get {
                return transform;
            }
        }
        public Transform wrist {
            get {
                //@TODO: NO magic enums!
                return laterality == GloveLaterality.GLOVE_LEFT
                    ? owner.leftWrist
                    : owner.rightWrist;
            }
        }

        private GameObject currentSpellPrefab;

        private Hand manusHand;

        private IEnumerable<ISpellState> currentStates {
            get {
                if (currentSpellPrefab == null) {
                    return Enumerable.Empty<ISpellState>();
                }
                if (!allStates.ContainsKey(currentSpellPrefab)) {
                    allStates[currentSpellPrefab] = Instantiate(currentSpellPrefab, wrist);
                }
                return allStates[currentSpellPrefab].GetComponents<ISpellState>();
            }
        }
        private Dictionary<GameObject, GameObject> allStates = new Dictionary<GameObject, GameObject>();

        private new Renderer renderer {
            get {
                if (rendererCache == null) {
                    rendererCache = GetComponentInChildren<Renderer>();
                }
                return rendererCache;
            }
        }
        private Renderer rendererCache;

        internal void Init(PlayerBehaviour playerBehaviour, GloveLaterality gloveLaterality) {
            owner = playerBehaviour;
            laterality = gloveLaterality;
            manusHand = GetComponentInChildren<Hand>();
        }

        public void SetGesture(Gesture gesture) {
            if (currentSpellPrefab == gesture.spellPrefab) {
                return;
            }
            currentStates.ForAll(state => state.OnExit(owner, this));
            currentSpellPrefab = gesture.spellPrefab;
            currentStates.ForAll(state => state.OnEnter(owner, this));
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            currentStates.ForAll(state => state.OnUpdate(owner, this));
        }
    }
}
