using UnityEngine;
using UnityEngine.Events;

namespace Carpocalypse
{
    public class Health : MonoBehaviour, IDamageable
    {
        [Header("Health Settings")]
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth;
        public bool isPlayer = false;

        [Header("Score")]
        public int scoreValue = 100;

        [Header("Events")]
        public UnityEvent onDeath;
        public UnityEvent<int> onHealthChanged;

        // IDamageable implementation
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsAlive => _currentHealth > 0;

        void Start()
        {
            _currentHealth = _maxHealth;

            // Fire initial health event for player
            if (isPlayer)
            {
                GameEvents.TriggerPlayerHealthChanged(_currentHealth, _maxHealth);
            }
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive) return;

            _currentHealth -= amount;
            _currentHealth = Mathf.Max(_currentHealth, 0);

            // Invoke events
            onHealthChanged?.Invoke(_currentHealth);

            if (isPlayer)
            {
                GameEvents.TriggerPlayerHealthChanged(_currentHealth, _maxHealth);
            }

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);

            onHealthChanged?.Invoke(_currentHealth);

            if (isPlayer)
            {
                GameEvents.TriggerPlayerHealthChanged(_currentHealth, _maxHealth);
            }
        }

        void Die()
        {
            onDeath?.Invoke();

            if (isPlayer)
            {
                GameEvents.TriggerPlayerDeath();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                }
            }
            else
            {
                GameEvents.TriggerEnemyKilled(gameObject, scoreValue);
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddScore(scoreValue);
                }
                Destroy(gameObject);
            }
        }

        public float GetHealthPercentage()
        {
            if (_maxHealth <= 0) return 0f;
            return (float)_currentHealth / (float)_maxHealth;
        }

        // Allow setting max health (useful for data-driven setup)
        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
            _currentHealth = value;
        }
    }
}
