using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Player.Gestures {
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