using PFVR.ScriptableObjects;
using UnityEngine;
namespace PFVR.Player.Gestures {
    /// <summary>
    /// The base class for all complex (i.e., two-hand) gestures.
    /// 
    /// Implementing scripts call <see cref="TurnOn"/> when they detect that hands have entered their trigger state, and <see cref="TurnOff"/> when they leave it.
    /// </summary>
    public class AbstractDetector : MonoBehaviour {
        [SerializeField]
        Gesture triggeredGesture = default;

        protected bool isOn { get; private set; } = false;
        protected bool isTurningOn { get; set; } = false;
        protected bool isTurningOff { get; set; } = false;

        [SerializeField, Range(0, 60)]
        int toggleFrames = 1;

        int startupFrames;
        int shutoffFrames;

        void FixedUpdate() {
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