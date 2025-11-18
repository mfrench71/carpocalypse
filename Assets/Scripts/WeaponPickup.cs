using UnityEngine;

namespace Carpocalypse
{
    public class WeaponPickup : MonoBehaviour
    {
        [Header("Pickup Settings")]
        public WeaponData weaponData;
        public float rotationSpeed = 50f;
        public float bobSpeed = 2f;
        public float bobHeight = 0.5f;

        private Vector3 startPosition;

        void Start()
        {
            startPosition = transform.position;

            if (weaponData == null)
            {
                Debug.LogWarning("WeaponPickup has no WeaponData assigned!");
            }
        }

        void Update()
        {
            // Rotate the pickup
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Bob up and down
            float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                WeaponSystem weaponSystem = other.GetComponent<WeaponSystem>();
                if (weaponSystem != null && weaponData != null)
                {
                    weaponSystem.AddWeapon(weaponData);
                    Destroy(gameObject);
                }
            }
        }
    }
}
