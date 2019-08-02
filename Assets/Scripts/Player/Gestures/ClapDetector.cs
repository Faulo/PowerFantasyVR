using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Player.Gestures {
    public class ClapDetector : AbstractDetector {
        [SerializeField]
        private bool isPrimary = false;

        private void OnTriggerEnter(Collider other) {
            if (isPrimary) {
                var otherDetector = other.GetComponent<ClapDetector>();
                if (otherDetector != null) {
                    if (isOn) {
                        isTurningOff = false;
                    } else {
                        isTurningOn = true;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            if (isPrimary) {
                var otherDetector = other.GetComponent<ClapDetector>();
                if (otherDetector != null) {
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