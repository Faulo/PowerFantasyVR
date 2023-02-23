using UnityEngine;

namespace PFVR.Player {
    [RequireComponent(typeof(PlayerBehaviour))]
    public class FPSController : MonoBehaviour {
        [SerializeField, Range(0, 1000)]
        float accelerationSpeed = 100;

        [SerializeField, Range(0, 1000)]
        float turnSpeed = 100;

        [SerializeField, Range(0, 1000)]
        float walkSpeed = 100;

        [SerializeField, Range(0, 1000)]
        float jumpSpeed = 100;

        [SerializeField, Range(0, 1000)]
        float fallSpeed = 100;

        PlayerBehaviour player;

        void Start() {
            player = GetComponent<PlayerBehaviour>();
        }

        void FixedUpdate() {
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed, 0);
            var velocity = transform.forward * Input.GetAxis("Vertical") * walkSpeed;
            if (Input.GetKey(KeyCode.Space)) {
                velocity += transform.up * jumpSpeed;
            } else {
                velocity -= transform.up * fallSpeed;
            }
            player.motor.LerpVelocity(velocity, Time.deltaTime * accelerationSpeed);
        }
    }
}
