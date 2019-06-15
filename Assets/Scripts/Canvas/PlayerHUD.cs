using PFVR.Player;
using PFVR.ScriptableObjects;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Canvas {
    public class PlayerHUD : MonoBehaviour {
        [SerializeField]
        private LayoutGroup leftGestureGroup;
        [SerializeField]
        private LayoutGroup rightGestureGroup;

        private PlayerBehaviour player => GetComponentInParent<PlayerBehaviour>();
        private ScriptableObjectManager<Gesture> leftGestureManager;
        private ScriptableObjectManager<Gesture> rightGestureManager;

        // Start is called before the first frame update
        void Start() {
            leftGestureManager = new ScriptableObjectManager<Gesture>(leftGestureGroup);
            leftGestureManager.OnlyShow(gesture => player.availableGestures.Contains(gesture));

            rightGestureManager = new ScriptableObjectManager<Gesture>(rightGestureGroup);
            rightGestureManager.OnlyShow(gesture => player.availableGestures.Contains(gesture));
        }

        // Update is called once per frame
        void Update() {

        }
    }
}