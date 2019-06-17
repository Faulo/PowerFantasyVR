using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PFVR.Canvas {
    public class BasicButton : MonoBehaviour {
        public bool selected {
            set {
                if (value) {
                    image.color = button.colors.selectedColor;
                    //EventSystem.current.SetSelectedGameObject(gameObject);
                } else {
                    image.color = button.colors.normalColor;
                }
            }
        }

        public UnityAction onClick {
            set {
                GetComponent<Button>().onClick.AddListener(value);
            }
        }
        public string text {
            get {
                return textMesh.text;
            }
            set {
                textMesh.text = value;
            }
        }
        private TextMeshProUGUI textMesh {
            get {
                return GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        private Image image => GetComponent<Image>();
        private Button button => GetComponent<Button>();
    }
}