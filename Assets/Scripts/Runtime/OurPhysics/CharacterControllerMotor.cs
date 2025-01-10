using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterControllerMotor : MonoBehaviour, IMotor {
        CharacterController characterController;

        [SerializeField, Range(0, 1)]
        float groundDrag = 0;

        [SerializeField, Range(0, 1)]
        float airDrag = 0;

        [SerializeField, Range(0, 10)]
        float gravityMultiplier = 1;

        public Vector3 position => transform.position;
        public Vector3 velocity { get; private set; }
        public float speed => velocity.magnitude;

        void Start() {
            characterController = GetComponent<CharacterController>();
        }

        void FixedUpdate() {
            if (characterController.isGrounded) {
                //apply ground drag
                Break(groundDrag);
                if (velocity.y > 0) {
                    velocity = new Vector3(velocity.x, 0, velocity.z);
                }
            } else {
                //apply air drag
                Break(airDrag);
                //apply gravity
                AddVelocity(gravityMultiplier * Physics.gravity * Time.deltaTime);
            }
            characterController.Move(velocity * Time.deltaTime);
        }

        public void AddVelocity(Vector3 addedVelocity) {
            velocity += addedVelocity;
        }
        public void Break(float breakFactor) {
            LerpVelocity(Vector3.zero, breakFactor);
        }
        public void LerpVelocity(Vector3 targetVelocity, float factor) {
            velocity = Vector3.Lerp(velocity, targetVelocity, factor);
        }
    }

}