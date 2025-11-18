using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;
    public int damage = 1;

    private float spawnTime;

    void OnEnable()
    {
        // Called when bullet is spawned from pool
        spawnTime = Time.time;
    }

    void Update()
    {
        // Move bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Deactivate after lifetime expires
        if (Time.time - spawnTime >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if we hit an enemy
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            // Add score when hitting enemy
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10);
            }
        }

        // Deactivate bullet when it hits something
        gameObject.SetActive(false);
    }
}
