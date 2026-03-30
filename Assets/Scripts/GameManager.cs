using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("World Speed")]
    public float worldSpeed = 3f;
    public float speedIncreaseRate = 0.08f;
    public float maxWorldSpeed = 7f;

    [Header("Game State")]
    public bool gameIsOver = false;
    public bool gameHasStarted = false;
    public bool isPaused = false;

    [Header("Difficulty")]
    public bool isPhaseTwo = false;

    [Header("Panels")]
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    [Header("Game Over UI")]
    public TMP_Text finalScoreText;

    [Header("UI Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public float buttonClickVolume = 0.8f;

    private void Awake()
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

    private void Start()
    {
        Time.timeScale = 1f;

        UpdateScoreUI();

        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        gameHasStarted = false;
        gameIsOver = false;
        isPaused = false;
        isPhaseTwo = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameHasStarted && !gameIsOver)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        if (!gameHasStarted)
            return;

        if (isPaused)
            return;

        if (!gameIsOver)
        {
            IncreaseSpeedOverTime();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void IncreaseSpeedOverTime()
    {
        float multiplier = isPhaseTwo ? 1.5f : 1f;

        worldSpeed += speedIncreaseRate * multiplier * Time.deltaTime;
        worldSpeed = Mathf.Clamp(worldSpeed, 0f, maxWorldSpeed);
    }

    public void StartGame()
    {
        PlayButtonClickSound();

        gameHasStarted = true;
        isPaused = false;
        Time.timeScale = 1f;

        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }
    }

    public void PauseGame()
    {
        if (!gameHasStarted || gameIsOver || isPaused)
            return;

        PlayButtonClickSound();

        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        PlayButtonClickSound();

        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        if (gameIsOver || !gameHasStarted) return;

        score += amount;
        UpdateScoreUI();

        if (!isPhaseTwo && score >= 10)
        {
            isPhaseTwo = true;
            Debug.Log("Phase 2 Activated!");
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void StopGame()
    {
        if (gameIsOver) return;

        gameIsOver = true;
        isPaused = false;
        Time.timeScale = 1f;

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + score;
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        CameraEffects effects = GetComponent<CameraEffects>();
        if (effects != null)
        {
            effects.PlayGameOverSound();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        PlayButtonClickSound();
        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        PlayButtonClickSound();
        StartCoroutine(QuitAfterDelay());
    }

    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound, buttonClickVolume);
        }
    }
}