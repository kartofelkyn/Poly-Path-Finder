// <copyright file="GameManagerUI.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// This script manages the player's score and lives, and updates the UI accordingly.
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int lives = 15;
    public int scoreAdd = 100;
    public int streakNumber = 0;
    public int streakAddition = 50;

    public float currentSpeed = 2f;
    public float maxSpeed = 50f;
    public float speedIncreaseRate = 0.1f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI difficultyText;
    string selectedDifficulty = LevelSelection.SelectedDifficulty;
    public GameObject gameOverPanel;
    public GameObject pauseResumePanel;
    public GameObject quizCompletePanel;
    public GameObject settingsPanel;
    public GameObject inputManager;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public GameState state = GameState.Intro;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (selectedDifficulty == "Hard")
        {
            lives = 3;
            score = 0;
            scoreAdd = 100;
            speedIncreaseRate = 0.2f;
            difficultyText.text = "Hard";
        }
        else if (selectedDifficulty == "Medium")
        {
            lives = 5;
            score = 0;
            scoreAdd = 50;
            speedIncreaseRate = 0.10f;
            difficultyText.text = "Medium";
        }
        else if (LevelSelection.useAI)
        {
            lives = 10;
            score = 0;
            scoreAdd = 50;
            speedIncreaseRate = 0f;
            difficultyText.text = "AI Mode";
        }
        else
        {
            lives = 8;
            score = 0;
            scoreAdd = 0;
            speedIncreaseRate = 0f;
            difficultyText.text = "Easy";
        }
        FootstepManager.instance.StartFootsteps();
        inputManager.SetActive(true);
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        UpdateUI();
        state = GameState.Intro;
    }
    void Update()
    {
        if (state != GameState.Playing) return;
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }
    public void AddScore(int amount)
    {

        int totalScore = amount + scoreAdd + (streakNumber * streakAddition);
        score += totalScore;

        Debug.Log("Score Added: " + totalScore + " (Base: " + amount + ", ScoreAdd: " + scoreAdd + ", StreakBonus: " + (streakNumber * streakAddition) + ")");
        
        streakNumber++;

        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowScore(score);
            PopupManager.Instance.ShowStreak(streakNumber);
        }


        UpdateUI();
    }
    public void PowerupAddScore(int amount)
    {
        score += amount;
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowScore(score);
        }
        UpdateUI();
    }
    public void PowerupAddStreak(int amount)
    {
        streakNumber += amount;

        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowStreak(streakNumber);
        }
        UpdateUI();
    }
    public void PowerupAddLife(int amount)
    {
        lives += amount;
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowLives(+1);
        }
        UpdateUI();
    }
    public void TakeDamage(int amount)
    {
        lives -= amount;
        streakNumber = 0;
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowStreak(streakNumber);
            PopupManager.Instance.ShowLives(-1);
        }
        if (lives <= 0)
        {
            UpdateUI();
            GameOver();
        }
        UpdateUI();
    }

    public void PauseResume()
    {
        if (state != GameState.Playing) return;

        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            FootstepManager.instance.StopFootsteps();
            inputManager.SetActive(false);
            pauseResumePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            FootstepManager.instance.StartFootsteps();
            inputManager.SetActive(true);
            pauseResumePanel.SetActive(false);
        }
    }
    public void showSettings()
    {
        Debug.Log("setting active");
        if (Time.timeScale == 0f)
        {
            settingsPanel.SetActive(true);
        }
    }
    public void backToPauseResume()
    {
        if (Time.timeScale == 0f)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        FootstepManager.instance.StopFootsteps();
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        SceneManager.LoadScene("GamePlay");
    }
    public void ReturnToMenu()
    {
        Debug.Log("SceneTransitionManager: " + SceneTransitionManager.Instance);

        FootstepManager.instance.StopFootsteps();
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        SceneTransitionManager.Instance.BeginTransition("MainMenu");
        Time.timeScale = 1f;
        FootstepManager.instance.StopFootsteps();
        pauseResumePanel.SetActive(false);
    }
    public void GameOver()
    {
        state = GameState.GameOver;
        Time.timeScale = 0f;
        FootstepManager.instance.StopFootsteps();
        FinalScoreUI();
        gameOverPanel.SetActive(true);
        inputManager.SetActive(false);
        currentSpeed = 0f;
        Debug.Log("Game Over!");
    }
    public void QuizComplete()
    {
        state = GameState.Victory;
        Time.timeScale = 0f;
        FootstepManager.instance.StopFootsteps();
        FinalScoreUI();
        quizCompletePanel.SetActive(true);
        inputManager.SetActive(false);
        currentSpeed = 0f;
        Debug.Log("Quiz Complete!");
    }
    void FinalScoreUI()
    {
        GamePlaySound.instance.GameEnd();
        SettingsManager.Instance.SetHighScore(score);

        currentScoreText.text = score.ToString("D5");
        highScoreText.text = SettingsManager.Instance.highScore.ToString("D5");

    }
    public void UpdateUI()
    {
        scoreText.text = score.ToString();
        liveText.text = lives.ToString();
    }

}
