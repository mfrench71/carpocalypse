using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Carpocalypse
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("UI References")]
        public Slider healthBar;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gameOverText;
        public GameObject gameOverPanel;

        private Health playerHealth;
        private int lastScore = -1;
        private float lastHealthPercent = -1f;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
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

            // Handle restart input
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GameManager.Instance.RestartGame();
                }
            }
        }

        void UpdateUI()
        {
            // Update health bar (only if changed)
            if (healthBar != null && playerHealth != null)
            {
                float healthPercent = playerHealth.GetHealthPercentage();
                if (healthPercent != lastHealthPercent)
                {
                    healthBar.value = healthPercent;
                    lastHealthPercent = healthPercent;
                }
            }

            // Update score (only if changed)
            if (scoreText != null && GameManager.Instance != null)
            {
                if (GameManager.Instance.score != lastScore)
                {
                    scoreText.text = "Score: " + GameManager.Instance.score;
                    lastScore = GameManager.Instance.score;
                }
            }

            // Show game over
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            {
                ShowGameOver();
            }
        }

        void ShowGameOver()
        {
            if (gameOverPanel != null && !gameOverPanel.activeSelf)
            {
                gameOverPanel.SetActive(true);
            }

            if (gameOverText != null && GameManager.Instance != null)
            {
                gameOverText.text = "GAME OVER\nFinal Score: " + GameManager.Instance.score + "\n\nPress R to Restart";
            }
        }
    }
}
