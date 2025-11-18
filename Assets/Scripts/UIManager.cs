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

        private int currentScore = 0;

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

        void OnEnable()
        {
            // Subscribe to events
            GameEvents.OnPlayerHealthChanged += UpdateHealthBar;
            GameEvents.OnScoreChanged += UpdateScore;
            GameEvents.OnGameOver += ShowGameOver;
            GameEvents.OnGameStart += OnGameStart;
        }

        void OnDisable()
        {
            // Unsubscribe from events
            GameEvents.OnPlayerHealthChanged -= UpdateHealthBar;
            GameEvents.OnScoreChanged -= UpdateScore;
            GameEvents.OnGameOver -= ShowGameOver;
            GameEvents.OnGameStart -= OnGameStart;
        }

        void Start()
        {
            // Hide game over panel at start
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }

            // Initialize score display
            UpdateScore(0);
        }

        void Update()
        {
            // Handle restart input
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GameManager.Instance.RestartGame();
                }
            }
        }

        void OnGameStart()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
        }

        void UpdateHealthBar(int current, int max)
        {
            if (healthBar != null && max > 0)
            {
                healthBar.value = (float)current / (float)max;
            }
        }

        void UpdateScore(int score)
        {
            currentScore = score;
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }

        void ShowGameOver()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            if (gameOverText != null)
            {
                gameOverText.text = "GAME OVER\nFinal Score: " + currentScore + "\n\nPress R to Restart";
            }
        }
    }
}
