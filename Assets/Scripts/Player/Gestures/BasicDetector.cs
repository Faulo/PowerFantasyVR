using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFVR.Player.Gestures {
    /// <summary>
    /// An example <see cref="AbstractDetctor"/> implementation. Detects when hands are within a certain distance towards each other.
    /// </summary>
    public class BasicDetector : AbstractDetector {
        [SerializeField]
        private Transform otherHandDetector = default;

        [SerializeField, Range(0, 10)]
        private float maximumDistance = 1;

        private void Update() {
            if (otherHandDetector) {
                if (Vector3.Distance(transform.position, otherHandDetector.position) < maximumDistance) {
                    if (isOn) {
                        isTurningOff = false;
                    } else {
                        isTurningOn = true;
                    }
                } else {
                    if (isOn) {
                        isTurningOff = true;
                    } else {
                        isTurningOn = false;
                    }
                }
            }
        }
        
        protected override void TurnOn() {
            //maximumDistance *= 2;
            base.TurnOn();
        }

        protected override void TurnOff() {
            //maximumDistance /= 2;
            base.TurnOff();
        }
    }
}