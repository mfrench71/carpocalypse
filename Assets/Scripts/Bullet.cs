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

        void OnEnable()
        {
            OnSpawnFromPool();
        }

        public void OnSpawnFromPool()
        {
            spawnTime = Time.time;
        }

        public void OnReturnToPool()
        {
            // Reset state
            isPlayerBullet = true;
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
                        gameObject.SetActive(false);
                        return;
                    }
                    else if (!isPlayerBullet && !health.isPlayer)
                    {
                        // Enemy bullet hit enemy - ignore
                        gameObject.SetActive(false);
                        return;
                    }
                }

                damageable.TakeDamage(damage);
            }

            // Deactivate bullet when it hits something
            gameObject.SetActive(false);
        }
    }
}
