using PFVR.Settings;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace PFVR.Player {
    public sealed class SpawnPoint : MonoBehaviour {
        InterfaceType playerType => GameSettings.instance.interfaceType;
        [SerializeField]
        GameObject basicPlayerPrefab = default;
        [SerializeField]
        GameObject vrPlayerPrefab = default;

        string xrDevice {
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

        GameObject playerPrefab {
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