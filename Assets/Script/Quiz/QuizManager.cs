// <copyright file="QuizManager.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
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
            useAIMode = true;
            aiQuizzes = AIQuizStorage.aiQuizzes;

            AIQuizStorage.aiQuizzes = null;
            LevelSelection.useAI = false;
            Debug.Log($"Quiz initialized with AI quizzes.");
            InitializeQuiz();
        }
        else
        {
            useAIMode = false;
            Debug.Log($"Quiz initialized with Built-in quizzes.");
            InitializeQuiz();
        }
    }
    public void InitializeQuiz()
    {
        Debug.Log("Selected Difficulty: " + LevelSelection.SelectedDifficulty);

        string selectedDiff = string.IsNullOrEmpty(LevelSelection.SelectedDifficulty)
            ? "Easy"
            : LevelSelection.SelectedDifficulty;

        if (useAIMode)
        {
            shuffledQuizzes = new List<Quiz>(aiQuizzes);
            Debug.Log($"Quiz shuffled with AI quizzes.");
        }
        else
        {
            shuffledQuizzes = quizData.GetQuizzesForDifficulty(selectedDiff);
            Debug.Log($"Quiz initialized with Built-in quizzes.");
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
        Debug.Log($"Quiz initialized with {shuffledQuizzes.Count} questions.");
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
        yield return new WaitUntil(() => isInitialized);

        if (shuffledQuizzes == null || shuffledQuizzes.Count == 0)
        {
            Debug.LogError("No quizzes loaded!");
            yield break;
        }

        if (currentQuestionIndex >= shuffledQuizzes.Count)
        {
            Debug.Log("Quiz Finished!");
            GameManager.Instance.QuizComplete();
            yield break;
        }

        Quiz selectedQuiz = shuffledQuizzes[currentQuestionIndex];
        currentQuestionIndex++;

        QuestionUIManager.Instance.questionText.text = selectedQuiz.question;

        // shuffle answers
        List<string> shuffledAnswers = new List<string>(selectedQuiz.answers);

        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            int rand = Random.Range(i, shuffledAnswers.Count);
            (shuffledAnswers[i], shuffledAnswers[rand]) = (shuffledAnswers[rand], shuffledAnswers[i]);
        }

        int newCorrectIndex = shuffledAnswers.IndexOf(selectedQuiz.answers[selectedQuiz.correctIndex]);

        float currentZ = distanceBetweenPlayer;

        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            currentZ += Random.Range(6f, 12f);

            Vector3 spawnPos = new Vector3(
                (Random.value < 0.5f) ? 0.5f : 1.5f,
                (Random.value < 0.5f) ? 1f : 2.25f,
                currentZ
            );

            GameObject gate = Instantiate(gateChoicesPrefab, spawnPos, Quaternion.identity);

            RenderQuizChoices container = gate.GetComponent<RenderQuizChoices>();
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

        Debug.Log("Spawned trigger: " + spawnedTrigger.name);
    }
}