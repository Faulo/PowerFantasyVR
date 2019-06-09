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
                    EventSystem.current.SetSelectedGameObject(gameObject);
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
    }
}