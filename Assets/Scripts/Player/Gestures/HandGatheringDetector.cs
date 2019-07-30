using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Player.Gestures {
    public class HandGatheringDetector : AbstractDetector {
        [HideInInspector]
        public bool isGathering = false;

        private void OnTriggerEnter(Collider other) {
            if (isGathering) {
                return;
            }
            var otherDetector = other.GetComponent<HandGatheringDetector>();
            if (otherDetector && !otherDetector.isGathering) {
                isGathering = true;
                otherDetector.isGathering = true;
                TurnOn();
            }
        }
        private void OnTriggerExit(Collider other) {
            if (!isGathering) {
                return;
            }
            var otherDetector = other.GetComponent<HandGatheringDetector>();
            if (otherDetector && otherDetector.isGathering) {
                isGathering = false;
                otherDetector.isGathering = false;
                TurnOff();
            }
        }
    }
}