using PFVR.Player;
using PFVR.ScriptableObjects;
using PFVR.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFVR.Canvas {
    /// <summary>
    /// The HUD used by the main menu, containing the level selection and options.
    /// </summary>
    public class MainMenuHUD : MonoBehaviour {
        [SerializeField]
        LayoutGroup levelSelectGroup = default;
        [SerializeField]
        TMP_Dropdown interfaceTypeDropdown = default;

        ScriptableObjectManager<Level> levelSelectManager;

        void Start() {
            levelSelectManager = new ScriptableObjectManager<Level>(levelSelectGroup, GameSettings.instance.levels);
            levelSelectManager.AddClickAction((level, button) => GameManager.instance.LoadLevel(level));

            interfaceTypeDropdown.value = (int)GameSettings.instance.interfaceType;
            interfaceTypeDropdown.onValueChanged.AddListener((value) => GameSettings.instance.interfaceType = (InterfaceType)value);
        }
    }
}