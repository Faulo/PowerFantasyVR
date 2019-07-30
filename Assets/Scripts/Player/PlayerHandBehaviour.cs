using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using PFVR.ScriptableObjects;
using PFVR.Spells;
using Slothsoft.UnityExtensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Player {
    public class PlayerHandBehaviour : MonoBehaviour {
        public PlayerBehaviour owner { get; private set; }
        public GloveLaterality laterality { get; private set; }
        public Transform tracker => transform;
        public Transform wrist;
        public Transform status;
        public Transform indexFinger;
        public Transform infinityStone;
        public Transform gatheringCenter;

        public bool enableDebugging;

        private GameObject currentSpellPrefab;

        private Hand manusHand;

        private IEnumerable<ISpellState> currentStates {
            get {
                if (currentSpellPrefab == null) {
                    return Enumerable.Empty<ISpellState>();
                }
                if (!allStates.ContainsKey(currentSpellPrefab)) {
                    allStates[currentSpellPrefab] = Instantiate(currentSpellPrefab, transform);
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
        private string statusText {
            get {
                return status.GetComponent<TextMesh>().text;
            }
            set {
                status.GetComponent<TextMesh>().text = value;
            }
        }

        internal void Init(PlayerBehaviour playerBehaviour, GloveLaterality gloveLaterality) {
            owner = playerBehaviour;
            laterality = gloveLaterality;
            manusHand = GetComponentInChildren<Hand>();
            statusText = "";
        }

        public void SetGesture(Gesture gesture) {
            string gestureName = gesture == null ? "???" : gesture.name;
            string spellName = (gesture == null || gesture.spellPrefab == null) ? "???" : gesture.spellPrefab.name;
            if (enableDebugging) {
                statusText = gestureName + ":\n" + spellName;
            }
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
        void FixedUpdate() {
            currentStates.ForAll(state => state.OnUpdate(owner, this));
        }

        void Update() {
        }
    }
}
