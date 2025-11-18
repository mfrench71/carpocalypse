using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    public Slider healthBar;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverPanel;

    private Health playerHealth;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Find player health component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }

        // Hide game over panel at start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Update health bar
        if (healthBar != null && playerHealth != null)
        {
            healthBar.value = playerHealth.GetHealthPercentage();
        }

        // Update score
        if (scoreText != null && GameManager.Instance != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.score;
        }

        // Show game over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverText != null && GameManager.Instance != null)
        {
            gameOverText.text = "GAME OVER\nFinal Score: " + GameManager.Instance.score + "\n\nPress R to Restart";
        }

        // Allow restart with R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.RestartGame();
        }
    }
}
