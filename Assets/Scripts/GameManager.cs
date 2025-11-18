using UnityEngine;
using UnityEngine.SceneManagement;

namespace Carpocalypse
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State")]
        public int score = 0;
        public bool isGameOver = false;

        [Header("References")]
        public GameObject playerPrefab;
        public Transform playerSpawnPoint;

        void Awake()
        {
            // Singleton pattern
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
            StartGame();
        }

        public void StartGame()
        {
            score = 0;
            isGameOver = false;
        }

        public void AddScore(int points)
        {
            if (isGameOver) return;
            score += points;
        }

        public void GameOver()
        {
            if (isGameOver) return;
            isGameOver = true;
        }

        public void RestartGame()
        {
            // Reset singleton before reload
            Instance = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
