using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMotor : MonoBehaviour, IMotor {
        private CharacterController characterController;

        [SerializeField, Range(0, 1)]
        private float groundDrag = 0;

        [SerializeField, Range(0, 1)]
        private float airDrag = 0;

        [SerializeField, Range(0, 10)]
        private float gravityMultiplier = 1;

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
        public void Move(Vector3 translation) {
            characterController.Move(translation);
        }
    }

}