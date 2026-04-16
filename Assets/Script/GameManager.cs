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

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public int scoreAdd = 100;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }
    public void AddScore(int amount)
    {
        score += amount + scoreAdd;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        lives -= amount;
        if (lives <= 0)
        {
            UpdateUI();
            GameOver();
        }
        UpdateUI();
    }

    public void PauseResume()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            FootstepManager.instance.StopFootsteps();
            pauseResumePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            FootstepManager.instance.StartFootsteps();
            pauseResumePanel.SetActive(false);
        }
    }
    public void showSettings()
    {
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
        else
        {
            lives = 3;
            score = 0;
            scoreAdd = 0;
            difficultyText.text = "Easy";
        }
        FootstepManager.instance.StartFootsteps();
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        UpdateUI();
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
        FootstepManager.instance.StopFootsteps();
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    void GameOver()
    {
        Time.timeScale = 0f;
        FootstepManager.instance.StopFootsteps();
        FinalScoreUI();
        gameOverPanel.SetActive(true);
        currentSpeed = 0f;
        Debug.Log("Game Over!");
    }
    public void QuizComplete()
    {
        Time.timeScale = 0f;
        FootstepManager.instance.StopFootsteps();
        FinalScoreUI();
        quizCompletePanel.SetActive(true);
        currentSpeed = 0f;
        Debug.Log("Quiz Complete!");
    }
    void FinalScoreUI()
    {
        GamePlaySound.instance.GameEnd();

        currentScoreText.text = score.ToString("D5");
        highScoreText.text = score.ToString("D5");
    }
    void UpdateUI()
    {
        scoreText.text = score.ToString();
        liveText.text = lives.ToString();
    }

}
