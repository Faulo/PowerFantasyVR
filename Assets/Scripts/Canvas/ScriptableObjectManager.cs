using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Canvas {

    public class ScriptableObjectManager<T> where T : ScriptableObject {
        private LayoutGroup root;
        private GameObject scriptablePrefab;
        private T[] scriptableObjects;
        private BasicButton[] scriptableButtons;

        public IEnumerable<T> elements {
            get {
                return scriptableObjects;
            }
        }

        public ScriptableObjectManager(LayoutGroup root) {
            this.root = root;
            scriptablePrefab = root.GetComponentInChildren<BasicButton>().gameObject;
            scriptablePrefab.SetActive(false);

            scriptableObjects = Resources.LoadAll<T>("ScriptableObjects");
            scriptableButtons = scriptableObjects
                .Select(obj => {
                    var button = UnityEngine.Object.Instantiate(scriptablePrefab, root.transform).GetComponent<BasicButton>();
                    button.text = obj.name;
                    button.gameObject.SetActive(true);
                    return button;
                })
                .ToArray();
        }

        public void AddClickAction(Action<T, BasicButton> action) {
            for (int i = 0; i < scriptableButtons.Length; i++) {
                var obj = scriptableObjects[i];
                var button = scriptableButtons[i];
                scriptableButtons[i].onClick = () => action(obj, button);
            }
        }

        public void OnlyShow(Func<T, bool> filter) {
            for (int i = 0; i < scriptableButtons.Length; i++) {
                scriptableButtons[i].gameObject.SetActive(filter(scriptableObjects[i]));
            }
        }
    }
}