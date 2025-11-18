using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    public bool isPlayer = false;

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

        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth + "/" + maxHealth);

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

        Debug.Log(gameObject.name + " healed " + amount + ". Health: " + currentHealth + "/" + maxHealth);
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
            Debug.Log("Player died! Game Over!");
        }
        else
        {
            // Enemy died - add score and destroy
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(100);
            }
            Destroy(gameObject);
        }
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }
}
