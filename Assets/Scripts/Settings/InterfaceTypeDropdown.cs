using PFVR.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PFVR.Settings {
    [RequireComponent(typeof(TMP_Dropdown))]
    public class InterfaceTypeDropdown : MonoBehaviour {
        void Start() {
            var dropdown = GetComponent<TMP_Dropdown>();
            dropdown.value = (int)GameSettings.instance.interfaceType;
            dropdown.onValueChanged.AddListener((int value) => GameSettings.instance.interfaceType = (InterfaceType) value);
        }
    }
}