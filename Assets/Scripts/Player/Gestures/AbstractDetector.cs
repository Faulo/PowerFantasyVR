﻿using PFVR.ScriptableObjects;
using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace PFVR.Player.Gestures {
    public class AbstractDetector : MonoBehaviour {
        [SerializeField]
        private Gesture triggeredGesture = default;

        protected bool isOn { get; private set; } = false;
        protected bool isTurningOn { get; set; } = false;
        protected bool isTurningOff { get; set; } = false;

        [SerializeField, Range(0, 60)]
        private int toggleFrames = 1;

        private int startupFrames;
        private int shutoffFrames;

        private void FixedUpdate() {
            if (isOn) {
                if (isTurningOff) {
                    shutoffFrames++;
                    if (shutoffFrames >= toggleFrames) {
                        shutoffFrames = 0;
                        TurnOff();
                    }
                } else {
                    if (shutoffFrames > 0) {
                        shutoffFrames--;
                    }
                }
            } else {
                if (isTurningOn) {
                    startupFrames++;
                    if (startupFrames >= toggleFrames) {
                        startupFrames = 0;
                        TurnOn();
                    }
                } else {
                    if (startupFrames > 0) {
                        startupFrames--;
                    }
                }
            }
        }

        protected virtual void TurnOn() {
            if (GestureConnector.instance.CanStartComplexGesture(triggeredGesture)) {
                isOn = true;
                isTurningOn = false;
                isTurningOff = false;
                GestureConnector.instance.StartComplexGesture(triggeredGesture);
            }
        }

        protected virtual void TurnOff() {
            isOn = false;
            isTurningOn = false;
            isTurningOff = false;
            GestureConnector.instance.StopComplexGesture(triggeredGesture);
        }
    }
}