using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public int score = 0;
    public bool isGameOver = false;

    [Header("References")]
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    private GameObject currentPlayer;

    void Awake()
    {
        // Singleton pattern - only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

        // We'll implement player spawning later
        Debug.Log("Game Started!");
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! Final Score: " + score);
    }

    public void RestartGame()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
