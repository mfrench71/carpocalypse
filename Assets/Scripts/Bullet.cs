using UnityEngine;

namespace Carpocalypse
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        public float speed = 20f;
        public float lifetime = 3f;
        public int damage = 1;
        public bool isPlayerBullet = true; // Track who fired this bullet

        private float spawnTime;
        private Collider bulletCollider;
        private GameObject shooter;

        void Awake()
        {
            bulletCollider = GetComponent<Collider>();
        }

        void OnEnable()
        {
            OnSpawnFromPool();
        }

        public void OnSpawnFromPool()
        {
            spawnTime = Time.time;
            shooter = null;
        }

        public void OnReturnToPool()
        {
            // Reset state
            isPlayerBullet = true;
            shooter = null;
        }

        // Call this after spawning to ignore collision with shooter
        public void SetShooter(GameObject shooterObj)
        {
            shooter = shooterObj;

            // Ignore collision with shooter
            if (bulletCollider != null && shooterObj != null)
            {
                Collider shooterCollider = shooterObj.GetComponent<Collider>();
                if (shooterCollider != null)
                {
                    Physics.IgnoreCollision(bulletCollider, shooterCollider, true);

                    // Re-enable collision after a short delay
                    Invoke(nameof(ReenableShooterCollision), 0.1f);
                }
            }
        }

        void ReenableShooterCollision()
        {
            if (bulletCollider != null && shooter != null)
            {
                Collider shooterCollider = shooter.GetComponent<Collider>();
                if (shooterCollider != null)
                {
                    Physics.IgnoreCollision(bulletCollider, shooterCollider, false);
                }
            }
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
            // Ignore other bullets
            if (collision.gameObject.GetComponent<Bullet>() != null)
            {
                return;
            }

            Debug.Log("Bullet hit: " + collision.gameObject.name + " (isPlayerBullet: " + isPlayerBullet + ")");

            // Try to damage anything with IDamageable interface
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Health health = collision.gameObject.GetComponent<Health>();

                // Player bullets don't damage player, enemy bullets don't damage enemies
                if (health != null)
                {
                    if (isPlayerBullet && health.isPlayer)
                    {
                        // Player bullet hit player - ignore
                        Debug.Log("Player bullet hit player - ignoring");
                        gameObject.SetActive(false);
                        return;
                    }
                    else if (!isPlayerBullet && !health.isPlayer)
                    {
                        // Enemy bullet hit enemy - ignore
                        Debug.Log("Enemy bullet hit enemy - ignoring");
                        gameObject.SetActive(false);
                        return;
                    }
                }

                Debug.Log("Bullet dealing " + damage + " damage to " + collision.gameObject.name);
                damageable.TakeDamage(damage);
            }

            // Deactivate bullet when it hits something
            gameObject.SetActive(false);
        }
    }
}
