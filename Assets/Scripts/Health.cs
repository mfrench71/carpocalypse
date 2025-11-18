using UnityEngine;
using UnityEngine.Events;

namespace Carpocalypse
{
    public class Health : MonoBehaviour
    {
        [Header("Health Settings")]
        public int maxHealth = 100;
        public int currentHealth;
        public bool isPlayer = false;

        [Header("Score")]
        public int scoreValue = 100;

        [Header("Events")]
        public UnityEvent onDeath;
        public UnityEvent<int> onHealthChanged;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);

            // Invoke health changed event
            onHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);

            onHealthChanged?.Invoke(currentHealth);
        }

        void Die()
        {
            onDeath?.Invoke();

            if (isPlayer)
            {
                // Player died - trigger game over
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                }
            }
            else
            {
                // Enemy died - add score and destroy
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddScore(scoreValue);
                }
                Destroy(gameObject);
            }
        }

        public float GetHealthPercentage()
        {
            if (maxHealth <= 0) return 0f;
            return (float)currentHealth / (float)maxHealth;
        }
    }
}
