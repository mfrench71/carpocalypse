using UnityEngine;

namespace Carpocalypse
{
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
            // Try to damage anything with a Health component (not just enemies)
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null && !health.isPlayer)
            {
                health.TakeDamage(damage);
            }

            // Deactivate bullet when it hits something
            gameObject.SetActive(false);
        }
    }
}
