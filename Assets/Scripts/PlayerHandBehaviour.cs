using ManusVR.Core.Apollo;
using PFVR.Gestures;
using PFVR.Tracking;
using System.Collections.Generic;
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

        private Gesture currentGesture;

        private IGestureState currentState {
            get {
                if (currentGesture == null) {
                    return null;
                }
                if (!states.ContainsKey(currentGesture)) {
                    states[currentGesture] = Instantiate(currentGesture.statePrefab, wrist).GetComponent<IGestureState>();
                }
                return states[currentGesture];
            }
        }
        private Dictionary<Gesture, IGestureState> states = new Dictionary<Gesture, IGestureState>();

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
        }

        public void SetGesture(Gesture gesture) {
            if (currentGesture == gesture) {
                return;
            }
            currentState?.OnExit(owner, this);
            currentGesture = gesture;
            currentState?.OnEnter(owner, this);

            if (renderer != null) {
                renderer.material = currentGesture.material;
            }
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            currentState?.OnUpdate(owner, this);
        }
    }
}
