using UnityEngine;

namespace PFVR.Player.Gestures {
    /// <summary>
    /// A script to detect when hands are in a Kame-Hame-Ha-like position, i.e., palms facing each other and 10-20 cm apart.
    /// </summary>
    public sealed class HandGatheringDetector : AbstractDetector {
        [SerializeField]
        bool isPrimary = false;

        new CapsuleCollider collider;

        void Start() {
            collider = GetComponent<CapsuleCollider>();
        }

        void OnTriggerEnter(Collider other) {
            if (isPrimary) {
                var otherDetector = other.GetComponent<HandGatheringDetector>();
                if (otherDetector != null) {
                    if (isOn) {
                        isTurningOff = false;
                    } else {
                        isTurningOn = true;
                    }
                }
            }
        }

        void OnTriggerExit(Collider other) {
            if (isPrimary) {
                var otherDetector = other.GetComponent<HandGatheringDetector>();
                if (otherDetector != null) {
                    if (isOn) {
                        isTurningOff = true;
                    } else {
                        isTurningOn = false;
                    }
                }
            }
        }

        protected override void TurnOn() {
            collider.height *= 4;
            base.TurnOn();
        }

        protected override void TurnOff() {
            collider.height /= 4;
            base.TurnOff();
        }
    }
}