using UnityEngine;

namespace Carpocalypse
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("AI Settings")]
        public float moveSpeed = 5f;
        public float rotationSpeed = 100f;
        public float stoppingDistance = 5f;

        [Header("Contact Damage")]
        public int damageAmount = 10;
        public float damageRate = 1f;

        private Transform player;
        private Rigidbody rb;
        private float nextDamageTime = 0f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("EnemyAI requires a Rigidbody component!");
            }

            FindPlayer();
        }

        void FindPlayer()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("No player found with 'Player' tag!");
            }
        }

        void FixedUpdate()
        {
            if (player == null || rb == null) return;

            // Calculate direction to player
            Vector3 direction = (player.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, player.position);

            // Only move if not too close
            if (distance > stoppingDistance)
            {
                // Move towards player
                Vector3 movement = direction * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);
            }

            // Rotate to face player
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }

        void OnCollisionStay(Collision collision)
        {
            // Damage player on contact
            if (collision.gameObject.CompareTag("Player") && Time.time >= nextDamageTime)
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    nextDamageTime = Time.time + damageRate;
                }
            }
        }
    }
}
