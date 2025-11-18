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
        public bool isPaused = false;

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

        void OnDestroy()
        {
            // Clear events when destroyed to prevent memory leaks
            if (Instance == this)
            {
                GameEvents.ClearAll();
            }
        }

        public void StartGame()
        {
            score = 0;
            isGameOver = false;
            isPaused = false;

            GameEvents.TriggerGameStart();
            GameEvents.TriggerScoreChanged(score);
        }

        public void AddScore(int points)
        {
            if (isGameOver) return;
            score += points;

            GameEvents.TriggerScoreAdded(points);
            GameEvents.TriggerScoreChanged(score);
        }

        public void GameOver()
        {
            if (isGameOver) return;
            isGameOver = true;

            GameEvents.TriggerGameOver();
        }

        public void PauseGame()
        {
            if (isGameOver) return;
            isPaused = true;
            Time.timeScale = 0f;

            GameEvents.TriggerGamePause();
        }

        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1f;

            GameEvents.TriggerGameResume();
        }

        public void RestartGame()
        {
            // Reset time scale in case we were paused
            Time.timeScale = 1f;

            // Clear events before reload
            GameEvents.ClearAll();

            // Reset singleton before reload
            Instance = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
