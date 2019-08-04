using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Player {
    public class FPSController : MonoBehaviour {
        [SerializeField, Range(0, 1000)]
        private float turnSpeed = 100;

        [SerializeField, Range(0, 1000)]
        private float walkSpeed = 100;

        [SerializeField, Range(0, 1000)]
        private float jumpSpeed = 100;

        [SerializeField, Range(0, 1000)]
        private float fallSpeed = 100;

        private CharacterController controller;

        void Start() {
            controller = GetComponent<CharacterController>();
        }

        void FixedUpdate() {
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed, 0);
            controller.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed);
            if (Input.GetKey(KeyCode.Space)) {
                controller.Move(transform.up * Time.deltaTime * jumpSpeed);
            } else {
                controller.Move(transform.up * Time.deltaTime * fallSpeed * -1);
            }
        }
    }
}
