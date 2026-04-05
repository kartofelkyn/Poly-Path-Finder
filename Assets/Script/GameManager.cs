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

    void Start()
    {
        if (selectedDifficulty == "Hard")
        {
            lives = 1;
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
            lives = 8;
            score = 0;
            scoreAdd = 0;
            difficultyText.text = "Easy";
        }
        gameOverPanel.SetActive(false);
        UpdateUI();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(2);
    }
    public void ReturnToMenu()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
    void GameOver()
    {
        Time.timeScale = 0f;
        FinalScoreUI();
        gameOverPanel.SetActive(true);
        currentSpeed = 0f;
        Debug.Log("Game Over!");
    }
    void FinalScoreUI()
    {
        currentScoreText.text = score.ToString();
        highScoreText.text = score.ToString();
    }
    void UpdateUI()
    {
        scoreText.text = score.ToString();
        liveText.text = lives.ToString();
    }

}
