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
        private LayoutGroup leftGestureGroup = default;
        [SerializeField]
        private LayoutGroup rightGestureGroup = default;
        [SerializeField]
        private GameObject iconPrefab = default;

        private PlayerBehaviour player => GetComponentInParent<PlayerBehaviour>();
        private ScriptableObjectManager<Gesture> leftGestureManager;
        private ScriptableObjectManager<Gesture> rightGestureManager;

        // Start is called before the first frame update
        void Start() {
            leftGestureManager = new ScriptableObjectManager<Gesture>(leftGestureGroup);
            leftGestureManager.OnlyShow(gesture => player.availableGestures.Contains(gesture));
            leftGestureManager.ForAll(DisplayIcon);

            rightGestureManager = new ScriptableObjectManager<Gesture>(rightGestureGroup);
            rightGestureManager.OnlyShow(gesture => player.availableGestures.Contains(gesture));
            rightGestureManager.ForAll(DisplayIcon);

            GestureConnector.onLeftGesture += leftGestureManager.SelectObject;
            GestureConnector.onRightGesture += rightGestureManager.SelectObject;
        }

        // Update is called once per frame
        void Update() {

        }

        private void DisplayIcon(Gesture gesture, BasicButton button) {
            if (gesture.icon != null) { 
                button.text = "";
                var icon = Instantiate(iconPrefab, button.transform);
                icon.GetComponent<Image>().sprite = gesture.icon;
            }
        }
    }
}