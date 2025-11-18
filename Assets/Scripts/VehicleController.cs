using UnityEngine;

namespace Carpocalypse
{
    public class VehicleController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 10f;
        public float rotationSpeed = 200f;

        private Rigidbody rb;
        private float moveInput;
        private float turnInput;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("VehicleController requires a Rigidbody component!");
            }
        }

        void Update()
        {
            // Get input from keyboard
            moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrows
            turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        }

        void FixedUpdate()
        {
            if (rb == null) return;

            // Move the vehicle forward/backward
            Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);

            // Rotate the vehicle left/right
            float rotation = turnInput * rotationSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}