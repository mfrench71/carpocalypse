using UnityEngine;

namespace Carpocalypse
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        public float speed = 20f;
        public float lifetime = 3f;
        public int damage = 1;

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
            // Reset any state if needed
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
                // Don't damage player (check Health component for isPlayer flag)
                Health health = collision.gameObject.GetComponent<Health>();
                if (health == null || !health.isPlayer)
                {
                    damageable.TakeDamage(damage);
                }
            }

            // Deactivate bullet when it hits something
            gameObject.SetActive(false);
        }
    }
}
