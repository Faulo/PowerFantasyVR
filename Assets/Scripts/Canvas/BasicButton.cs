using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PFVR.Canvas {
    /// <summary>
    /// A wrapper for <see cref="Button"/>. 
    /// </summary>
    public class BasicButton : MonoBehaviour {
        public bool selected {
            set {
                if (value) {
                    image.color = button.colors.selectedColor;
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
        TextMeshProUGUI textMesh {
            get {
                return GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        Image image => GetComponent<Image>();
        Button button => GetComponent<Button>();
    }
}