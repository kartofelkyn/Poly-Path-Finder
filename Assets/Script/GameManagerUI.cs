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
    private void Awake()
    {
        Instance = this;
    }
    public void AddScore(int amount)
    {
        score += amount;
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

    void UpdateUI()
    {
        scoreText.text = score.ToString();
        liveText.text = lives.ToString();
    }

}
