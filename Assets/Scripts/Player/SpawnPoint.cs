using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;
using Valve.VR;

namespace PFVR.Player {
    public class SpawnPoint : MonoBehaviour {
        public enum PlayerType {
            Basic,
            VR
        }
        [SerializeField]
        private PlayerType playerType = default;
        [SerializeField]
        private GameObject basicPlayerPrefab = default;
        [SerializeField]
        private GameObject vrPlayerPrefab = default;

        private string xrDevice {
            get {
                switch (playerType) {
                    case PlayerType.Basic:
                        return "None";
                    case PlayerType.VR:
                        return "OpenVR";
                }
                throw new System.Exception("???" + playerType);
            }
        }

        private GameObject playerPrefab {
            get {
                switch (playerType) {
                    case PlayerType.Basic:
                        return basicPlayerPrefab;
                    case PlayerType.VR:
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
                case PlayerType.Basic:
                    if (XRSettings.loadedDeviceName != xrDevice) {
                        XRSettings.LoadDeviceByName(xrDevice);
                    }
                    break;
                case PlayerType.VR:
                    if (XRSettings.loadedDeviceName != xrDevice) {
                        XRSettings.LoadDeviceByName(xrDevice);
                        SteamVR.Initialize(true);
                    }
                    break;
            }
            
        }
    }
}