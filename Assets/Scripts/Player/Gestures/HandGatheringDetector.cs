using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Player.Gestures {
    /// <summary>
    /// A script to detect when hands are in a Kame-Hame-Ha-like position, i.e., palms facing each other and 10-20 cm apart.
    /// </summary>
    public class HandGatheringDetector : AbstractDetector {
        [SerializeField]
        private bool isPrimary = false;

        private new CapsuleCollider collider;

        private void Start() {
            collider = GetComponent<CapsuleCollider>();
        }

        private void OnTriggerEnter(Collider other) {
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

        private void OnTriggerExit(Collider other) {
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