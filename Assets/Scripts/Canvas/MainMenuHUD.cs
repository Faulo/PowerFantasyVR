using PFVR.Player;
using PFVR.ScriptableObjects;
using PFVR.Settings;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Canvas {
    public class MainMenuHUD : MonoBehaviour {
        [SerializeField]
        private LayoutGroup levelSelectGroup = default;
        [SerializeField]
        private TMP_Dropdown interfaceTypeDropdown = default;

        private ScriptableObjectManager<Level> levelSelectManager;

        void Start() {
            levelSelectManager = new ScriptableObjectManager<Level>(levelSelectGroup, GameSettings.instance.levels);
            levelSelectManager.AddClickAction((level, button) => {
                GameManager.instance.LoadLevel(level);
            });

            interfaceTypeDropdown.value = (int)GameSettings.instance.interfaceType;
            interfaceTypeDropdown.onValueChanged.AddListener((int value) => GameSettings.instance.interfaceType = (InterfaceType)value);

        }
    }
}