using UnityEngine;

namespace PFVR.Player.Gestures {
    /// <summary>
    /// A script to detect when hands are in a clapping position, i.e., palms facing each other and touching.
    /// </summary>
    public sealed class ClapDetector : AbstractDetector {
        [SerializeField]
        bool isPrimary = false;

        void OnTriggerEnter(Collider other) {
            if (isPrimary) {
                if (other.TryGetComponent<ClapDetector>(out var otherDetector)) {
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
                if (other.TryGetComponent<ClapDetector>(out var otherDetector)) {
                    if (isOn) {
                        isTurningOff = true;
                    } else {
                        isTurningOn = false;
                    }
                }
            }
        }
    }
}