using UnityEngine;

namespace Carpocalypse
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Follow Settings")]
        public Transform target;
        public Vector3 offset = new Vector3(0f, 15f, -10f);
        public float smoothSpeed = 5f;

        void Start()
        {
            // Auto-find player if target not assigned
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                }
                else
                {
                    Debug.LogWarning("CameraFollow: No target assigned and no Player found!");
                }
            }
        }

        void LateUpdate()
        {
            if (target == null) return;

            // Calculate desired position
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move camera to desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}