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
    public int streakAddition = 5;

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

    // This function initializes and checks the selected difficulty level, 
    // setting the player's lives, score, score addition, speed increase rate, 
    // and updating the difficulty text in the UI accordingly. It also starts 
    // the footstep sounds and sets the game state to Intro.
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
        // Start the footstep sounds when the game starts
        FootstepManager.instance.StartFootsteps();
        // Initialize the panel activate states and set the game state to Intro

        inputManager.SetActive(true);
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        UpdateUI();
        state = GameState.Intro;
    }
    
    // This function is called once per frame and is responsible for increasing the player's speed over time while the game state is Playing.
    void Update()
    {
        if (state != GameState.Playing) return;
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    // This function adds score to the player's total score based on the base amount, 
    // any additional score from streaks, and updates the UI accordingly. It also increments the streak number 
    // and shows the score and streak in the popup manager for feedback.
    public void AddScore(int amount)
    {

        int totalScore = amount + scoreAdd + (streakNumber * streakAddition);
        score += totalScore;

        // Debug.Log("Score Added: " + totalScore + " (Base: " + amount + ", ScoreAdd: " + scoreAdd + ", StreakBonus: " + (streakNumber * streakAddition) + ")");

        streakNumber++;

        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowScore(totalScore);
            PopupManager.Instance.ShowStreak(streakNumber);
        }

        UpdateUI();
    }

    // These functions are used to add score, streaks, and lives from power-ups, as well as to handle taking damage. 
    // They update the UI and show popups for feedback when these events occur. The TakeDamage function also checks 
    // for game over conditions when lives reach zero.
    public void PowerupAddScore(int amount)
    {
        score += amount;
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowScore(score);
        }
        UpdateUI();
    }

    // This function adds to the player's streak count and updates the UI accordingly. 
    // It also shows the current streak in the popup manager for feedback.
    public void PowerupAddStreak(int amount)
    {
        streakNumber += amount;

        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowStreak(streakNumber);
        }
        UpdateUI();
    }

    // This function adds lives to the player and updates the UI accordingly. It also shows the change in lives in the popup manager for feedback. 
    public void PowerupAddLife(int amount)
    {
        lives += amount;
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowLives(+1);
        }
        UpdateUI();
    }

    // This function handles the player taking damage by reducing lives, resetting the streak, and checking for game over conditions. 
    // It also updates the UI and shows the change in lives and streak in the popup manager for feedback.
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

    // This function toggles the pause and resume state of the game. 
    // When the game is paused, it stops the footstep sounds, disables player input, and shows the pause menu. 
    // It also sets the time scale to 0 to freeze the game.
    // When the game is resumed, it restarts the footstep sounds, enables player input, and hides the pause menu.
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

    // This function shows the settings panel when the game is paused, allowing the player to adjust settings while the game is not active.
    public void showSettings()
    {
        // Debug.Log("setting active");
        if (Time.timeScale == 0f)
        {
            settingsPanel.SetActive(true);
        }
    }

    // This function hides the settings panel and returns to the pause menu when the player is done adjusting settings.
    public void backToPauseResume()
    {
        if (Time.timeScale == 0f)
        {
            settingsPanel.SetActive(false);
        }
    }

    // This function restarts the game by stopping footstep sounds, resetting the time scale, 
    // hiding game over and quiz complete panels, and loading the gameplay scene again.
    public void RestartGame()
    {
        FootstepManager.instance.StopFootsteps();
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        SceneManager.LoadScene("GamePlay");
    }

    // This function returns the player to the main menu by stopping footstep sounds, hiding game over 
    // and quiz complete panels, and initiating a scene transition to the main menu. It also resets the time scale and hides the pause
    public void ReturnToMenu()
    {
        // Debug.Log("SceneTransitionManager: " + SceneTransitionManager.Instance);

        FootstepManager.instance.StopFootsteps();
        gameOverPanel.SetActive(false);
        quizCompletePanel.SetActive(false);
        SceneTransitionManager.Instance.BeginTransition("MainMenu");
        Time.timeScale = 1f;
        FootstepManager.instance.StopFootsteps();
        pauseResumePanel.SetActive(false);
    }

    // This function handles the game over state by stopping footstep sounds, setting the time scale to 0, 
    // showing the game over panel, disabling player input, and updating the final score UI. It also logs 
    // a message indicating that the game is over.
    public void GameOver()
    {
        state = GameState.GameOver;
        Time.timeScale = 0f;
        FootstepManager.instance.StopFootsteps();
        FinalScoreUI();
        gameOverPanel.SetActive(true);
        inputManager.SetActive(false);
        currentSpeed = 0f;
        // Debug.Log("Game Over!");
    }

    // This function handles the quiz completion state by stopping footstep sounds, setting the time scale to 0,
    // showing the quiz complete panel, disabling player input, and updating the final score UI. 
    // It also logs a message indicating that the quiz is complete.
    public void QuizComplete()
    {
        state = GameState.Victory;
        Time.timeScale = 0f;
        FootstepManager.instance.StopFootsteps();


        quizCompletePanel.SetActive(true);
        inputManager.SetActive(false);

        FinalScoreUI();

        currentSpeed = 0f;
        // Debug.Log("Quiz Complete!");
    }

    // This function updates the final score UI by setting the current score and high 
    // score text based on the player's score and the stored high score. It also plays 
    // the game end sound and logs the final score for debugging purposes.
    void FinalScoreUI()
    {
        GamePlaySound.instance.GameEnd();
        SettingsManager.Instance.SetHighScore(score);
        // Debug.Log("Final score"+ score);
        currentScoreText.text = score.ToString("D5");
        highScoreText.text = SettingsManager.Instance.highScore.ToString("D5");

    }

    // This function updates the score and lives text in the UI to reflect the current values.
    public void UpdateUI()
    {
        scoreText.text = score.ToString();
        liveText.text = lives.ToString();
    }

}
