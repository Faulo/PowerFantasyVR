using PFVR.Player;
using PFVR.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Canvas {
    /// <summary>
    /// The in-game HUD, containing unlocked gestures and such.
    /// </summary>
    public class PlayerHUD : MonoBehaviour {
        [SerializeField]
        LayoutGroup leftGestureGroup = default;
        [SerializeField]
        LayoutGroup rightGestureGroup = default;
        [SerializeField]
        GameObject iconPrefab = default;

        GestureConnector player => GetComponentInParent<GestureConnector>();
        ScriptableObjectManager<Gesture> leftGestureManager;
        ScriptableObjectManager<Gesture> rightGestureManager;

        // Start is called before the first frame update
        void Start() {
            leftGestureManager = new ScriptableObjectManager<Gesture>(leftGestureGroup);
            //leftGestureManager.OnlyShow(gesture => player.IsUnlocked(gesture));
            leftGestureManager.ForAll(DisplayIcon);

            rightGestureManager = new ScriptableObjectManager<Gesture>(rightGestureGroup);
            //rightGestureManager.OnlyShow(gesture => player.IsUnlocked(gesture));
            rightGestureManager.ForAll(DisplayIcon);

            GestureConnector.onLeftGesture += leftGestureManager.SelectObject;
            GestureConnector.onRightGesture += rightGestureManager.SelectObject;
        }

        // Update is called once per frame
        void Update() {
            leftGestureManager.OnlyShow(gesture => player.IsUnlocked(gesture));
            rightGestureManager.OnlyShow(gesture => player.IsUnlocked(gesture));
        }

        void DisplayIcon(Gesture gesture, BasicButton button) {
            if (gesture.icon != null) {
                button.text = "";
                var icon = Instantiate(iconPrefab, button.transform);
                icon.GetComponent<Image>().sprite = gesture.icon;
            }
            var colors = button.GetComponent<Button>().colors;
            colors.selectedColor = gesture.spellColor;
            button.GetComponent<Button>().colors = colors;
        }
    }
}