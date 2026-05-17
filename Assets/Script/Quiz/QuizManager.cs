// <copyright file="QuizManager.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// This script is responsible for managing the quiz system in the game. 
/// It handles the loading of quizzes, spawning of quiz gates, and tracking 
/// the player's progress through the quiz.
/// The QuizManager loads quizzes from a QuizData object, shuffles them, and 
/// spawns quiz gates with the questions and answers as the player progresses 
/// through the game. It also handles the logic for checking if the player's 
/// answer is correct and updating the game state accordingly.
/// The QuizManager uses a singleton pattern to allow other scripts to easily 
/// access its functionality. It also supports loading quizzes from an AI source, 
/// allowing for dynamic quiz content based on player preferences or other factors.
/// </summary>

public class QuizManager : MonoBehaviour
{
    public QuizData quizData;
    public GameObject gateChoicesPrefab;
    public GameObject newQuizTrigger;
    public GameObject checkIfanswerTrigger;
    public bool questionAnswered = false;
    public bool checkIfAnswerCalled = false;
    private float lastSpawnGateZ = 20f;
    public float distanceBetweenGates = 0f;
    private float distanceBetweenPlayer = 20f;
    private int currentQuestionIndex = 0;
    private List<Quiz> shuffledQuizzes;
    public static QuizManager Instance;
    private List<Quiz> aiQuizzes;
    private bool useAIMode = false;
    public bool isInitialized = false;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        if (LevelSelection.useAI)
        {
            useAIMode = true;
        }

        if (LevelSelection.useAI && AIQuizStorage.aiQuizzes != null && AIQuizStorage.aiQuizzes.Count > 0)
        {
            // If AI quizzes are available, use them and initialize the quiz
            useAIMode = true;
            aiQuizzes = AIQuizStorage.aiQuizzes;
            AIQuizStorage.aiQuizzes = null;
            LevelSelection.useAI = false;
            InitializeQuiz();
        }
        else
        {
            // If no AI quizzes are available, fall back to built-in quizzes
            useAIMode = false;
            InitializeQuiz();
        }
    }
    public void InitializeQuiz()
    {
        string selectedDiff = string.IsNullOrEmpty(LevelSelection.SelectedDifficulty)
            ? "Easy"
            : LevelSelection.SelectedDifficulty;

        if (useAIMode)
        {
            shuffledQuizzes = new List<Quiz>(aiQuizzes);
            // Debug.Log($"Quiz shuffled with AI quizzes.");
        }
        else
        {
            shuffledQuizzes = quizData.GetQuizzesForDifficulty(selectedDiff);
            // Debug.Log($"Quiz initialized with Built-in quizzes.");
        }

        // Shuffle
        for (int i = 0; i < shuffledQuizzes.Count; i++)
        {
            int rand = Random.Range(i, shuffledQuizzes.Count);
            var temp = shuffledQuizzes[i];
            shuffledQuizzes[i] = shuffledQuizzes[rand];
            shuffledQuizzes[rand] = temp;
        }

        currentQuestionIndex = 0;
        isInitialized = true;
        // Debug.Log($"Quiz initialized with {shuffledQuizzes.Count} questions.");
    }

    public void LoadAIQuizzes(List<Quiz> quizzes)
    {
        useAIMode = true;
        aiQuizzes = quizzes;

        InitializeQuiz();
    }
    public void StartBuiltInQuiz()
    {
        useAIMode = false;
        InitializeQuiz();
    }

    public void SpawnNextQuiz()
    {
        StartCoroutine(WaitAndSpawn());
    }

    private IEnumerator WaitAndSpawn()
    {
        // Wait until the quiz is initialized before proceeding
        yield return new WaitUntil(() => isInitialized);

        // Check if there are quizzes available before trying to spawn the next one to avoid errors
        if (shuffledQuizzes == null || shuffledQuizzes.Count == 0)
        {
            Debug.LogError("No quizzes loaded!");
            yield break;
        }

        // Check if we've gone through all the quizzes
        if (currentQuestionIndex >= shuffledQuizzes.Count)
        {
            // Debug.Log("Quiz Finished!");
            GameManager.Instance.QuizComplete();
            yield break;
        }

        // Select the next quiz question and answers
        Quiz selectedQuiz = shuffledQuizzes[currentQuestionIndex];
        currentQuestionIndex++;

        // Display the question text in the UI
        QuestionUIManager.Instance.questionText.text = selectedQuiz.question;

        // shuffle answers
        List<string> shuffledAnswers = new List<string>(selectedQuiz.answers);

        // Shuffle the answers using the Fisher-Yates algorithm
        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            int rand = Random.Range(i, shuffledAnswers.Count);
            (shuffledAnswers[i], shuffledAnswers[rand]) = (shuffledAnswers[rand], shuffledAnswers[i]);
        }

        // Determine the new index of the correct answer after shuffling
        int newCorrectIndex = shuffledAnswers.IndexOf(selectedQuiz.answers[selectedQuiz.correctIndex]);

        // Spawn gates for each answer choice at increasing distances from the player
        float currentZ = distanceBetweenPlayer;
        
        // This loop spawns the gates for the quiz answers, positioning them at random X and Y 
        // coordinates while increasing the Z coordinate to space them out along the player's path. 
        // Each gate is set up with the corresponding answer choice and its index for later checking 
        // when the player interacts with it.
        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            currentZ += Random.Range(6f, 12f); // Increase Z to space out the gates, with some randomness for variety

            Vector3 spawnPos = new Vector3(
                (Random.value < 0.5f) ? 0.5f : 1.5f, // Randomly choose between two X positions for the gates (lanes)
                (Random.value < 0.5f) ? 1f : 2.25f, // Randomly choose between two Y positions for the gates (heights)
                currentZ
            );

            GameObject gate = Instantiate(gateChoicesPrefab, spawnPos, Quaternion.identity);

            RenderQuizChoices container = gate.GetComponent<RenderQuizChoices>(); // Get the RenderQuizChoices component from the instantiated gate prefab
            if (container != null)
            {
                container.Setup(shuffledAnswers[i], newCorrectIndex, i);
            }
        }

        float finalGateZ = currentZ;

        Instantiate(checkIfanswerTrigger, new Vector3(1f, 1f, finalGateZ + 5f), Quaternion.identity);

        GameObject spawnedTrigger = Instantiate(
            newQuizTrigger,
            new Vector3(1f, 1f, finalGateZ + 15f),
            Quaternion.identity
        );

        //Debug.Log("Spawned trigger: " + spawnedTrigger.name);
    }
}