// <copyright file="GameManagerUI.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;

/// <summary>
/// This script manages the player's score and lives, and updates the UI accordingly.
/// </summary>

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI Instance;
    public int score = 0;
    public int lives = 15;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    string selectedDifficulty = LevelSelection.SelectedDifficulty;
    public int scoreAdd = 100;
    private void Awake()
    {
        Instance = this;
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
            lives = 0;
            Debug.Log("Game Over!");
        }
        UpdateUI();
    }

    void Start()
    {
        if (selectedDifficulty == "Hard")
        {
            lives = 3;
            score = 0;
            scoreAdd = 100;
        }else if (selectedDifficulty == "Medium")
        {
            lives = 5;
            score = 0;
            scoreAdd = 50;
        }
        else
        {
            lives = 8;
            score = 0;
            scoreAdd = 0;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
        liveText.text = lives.ToString();
    }

}
