﻿using System.Collections.Generic;
using System.Linq;
using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using PFVR.ScriptableObjects;
using PFVR.Spells;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.Player {
    public sealed class PlayerHandBehaviour : MonoBehaviour {
        public PlayerBehaviour owner { get; private set; }
        public GloveLaterality laterality { get; private set; }
        public Transform tracker => transform;
        public Transform wrist;
        public Transform status;
        public Transform middleFinger;
        public Transform indexFinger;
        public Transform infinityStone;
        public Transform gatheringCenter;

        public bool enableDebugging;

        GameObject currentSpellPrefab;

        Hand manusHand;

        [SerializeField, Range(0, 100)]
        float shakeMinSpeed = 1;
        [SerializeField, Range(0, 100)]
        float shakeMinAngle = 1;
        public bool isShaking => angle > shakeMinAngle && velocity.magnitude > shakeMinSpeed;

        [SerializeField, Range(0, 100)]
        float accelerationUpdateSpeed = 1;
        public Vector3 acceleration { get; private set; }
        Vector3 oldVelocity = Vector3.zero;

        [SerializeField, Range(0, 100)]
        float velocityUpdateSpeed = 1;
        public Vector3 velocity { get; private set; }
        Vector3 oldPosition = Vector3.zero;

        [SerializeField, Range(0, 100)]
        float angleUpdateSpeed = 1;
        public float angle { get; private set; }
        Quaternion oldRotation = Quaternion.identity;

        IEnumerable<ISpellState> currentStates {
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
        Dictionary<GameObject, GameObject> allStates = new Dictionary<GameObject, GameObject>();

        new Renderer renderer {
            get {
                if (rendererCache == null) {
                    rendererCache = GetComponentInChildren<Renderer>();
                }

                return rendererCache;
            }
        }
        Renderer rendererCache;

        string statusText {
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
            velocity = Vector3.Lerp(velocity, transform.position - oldPosition, velocityUpdateSpeed * Time.deltaTime);
            oldPosition = transform.position;

            acceleration = Vector3.Lerp(acceleration, velocity - oldVelocity, accelerationUpdateSpeed * Time.deltaTime);
            oldVelocity = velocity;

            angle = Mathf.Lerp(angle, Quaternion.Angle(transform.rotation, oldRotation), angleUpdateSpeed * Time.deltaTime);
            oldRotation = transform.rotation;

            currentStates.ForAll(state => state.OnUpdate(owner, this));
        }

        void Update() {
        }
    }
}