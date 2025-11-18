using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float stoppingDistance = 5f;

    [Header("Damage")]
    public int damageAmount = 10;
    public float damageRate = 1f;
    private float nextDamageTime = 0f;

    private Rigidbody rb;
    private Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();

        // Find player automatically if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

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

    public void TakeDamage(int damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
