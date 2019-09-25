using PFVR.ScriptableObjects;
using PFVR.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;
using Valve.VR;

namespace PFVR.Player {
    public partial class SpawnPoint : MonoBehaviour {
        private InterfaceType playerType => GameSettings.instance.interfaceType;
        [SerializeField]
        private GameObject basicPlayerPrefab = default;
        [SerializeField]
        private GameObject vrPlayerPrefab = default;

        private string xrDevice {
            get {
                switch (playerType) {
                    case InterfaceType.MouseAndKeyboard:
                        return "None";
                    case InterfaceType.ManusVR:
                        return "OpenVR";
                }
                throw new System.Exception("???" + playerType);
            }
        }

        private GameObject playerPrefab {
            get {
                switch (playerType) {
                    case InterfaceType.MouseAndKeyboard:
                        return basicPlayerPrefab;
                    case InterfaceType.ManusVR:
                        return vrPlayerPrefab;
                }
                throw new System.Exception("???" + playerType);
            }
        }

        void Awake() {
            Instantiate(playerPrefab, transform.position, transform.rotation);
        }

        void Start() {
            switch (playerType) {
                case InterfaceType.MouseAndKeyboard:
                    if (XRSettings.loadedDeviceName != xrDevice) {
                        XRSettings.LoadDeviceByName(xrDevice);
                    }
                    break;
                case InterfaceType.ManusVR:
                    if (XRSettings.loadedDeviceName != xrDevice) {
                        XRSettings.LoadDeviceByName(xrDevice);
                        SteamVR.Initialize(true);
                    }
                    break;
            }
            
        }
    }
}