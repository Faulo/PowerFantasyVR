using ManusVR.Core.Apollo;
using PFVR.Gestures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.DataModels {
    public class PlayerHandBehaviour : MonoBehaviour {
        public PlayerBehaviour owner { get; private set; }
        public GloveLaterality laterality { get; private set; }

        public Gesture gesture {
            set {
                if (currentGesture == value) {
                    return;
                }
                currentState?.OnExit(owner, this);
                currentGesture = value;
                currentState?.OnEnter(owner, this);

                renderer.material = currentGesture.material;
            }
        }

        private Gesture currentGesture;
        private Dictionary<Gesture, IGestureState> states = new Dictionary<Gesture, IGestureState>();
        private IGestureState currentState {
            get {
                if (currentGesture == null) {
                    return null;
                }
                if (!states.ContainsKey(currentGesture)) {
                    states[currentGesture] = Instantiate(currentGesture.statePrefab, transform).GetComponent<IGestureState>();
                }
                return states[currentGesture];
            }
        }
        private new Renderer renderer {
            get {
                return GetComponentInChildren<Renderer>();
            }
        }

        internal void Init(PlayerBehaviour playerBehaviour, GloveLaterality gloveLaterality) {
            owner = playerBehaviour;
            laterality = gloveLaterality;
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
