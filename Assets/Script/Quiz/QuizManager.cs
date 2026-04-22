// <copyright file="QuizManager.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Debug.Log("Selected Difficulty: " + LevelSelection.SelectedDifficulty);
        string selectedDiff = string.IsNullOrEmpty(LevelSelection.SelectedDifficulty)
    ? "Easy"
    : LevelSelection.SelectedDifficulty;

        // Filter quizzes based on the logic:
        // Easy -> Easy | Medium -> Easy + Medium | Hard -> All
        shuffledQuizzes = quizData.GetQuizzesForDifficulty(selectedDiff);

        // Shuffle the filtered list
        for (int i = 0; i < shuffledQuizzes.Count; i++)
        {
            int rand = Random.Range(i, shuffledQuizzes.Count);
            var temp = shuffledQuizzes[i];
            shuffledQuizzes[i] = shuffledQuizzes[rand];
            shuffledQuizzes[rand] = temp;
        }

        Debug.Log($"Quiz initialized with {shuffledQuizzes.Count} questions for {selectedDiff} mode.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnNextQuiz();
        }
    }

    public void SpawnNextQuiz()
    {
        if (shuffledQuizzes == null || currentQuestionIndex >= shuffledQuizzes.Count)
        {
            Debug.Log("Quiz Finished!");
            GameManager.Instance.QuizComplete();
            return;
        }

        Quiz selectedQuiz = shuffledQuizzes[currentQuestionIndex];
        currentQuestionIndex++;

        // Set question text
        QuestionUIManager.Instance.questionText.text = selectedQuiz.question;

        // SHUFFLE ANSWERS
        List<string> shuffledAnswers = new List<string>(selectedQuiz.answers);

        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            int rand = Random.Range(i, shuffledAnswers.Count);
            string temp = shuffledAnswers[i];
            shuffledAnswers[i] = shuffledAnswers[rand];
            shuffledAnswers[rand] = temp;
        }

        // Find new correct index after shuffle
        int newCorrectIndex = shuffledAnswers.IndexOf(
            selectedQuiz.answers[selectedQuiz.correctIndex]
        );

        // SPAWN GATES
        float currentZ = distanceBetweenPlayer;

        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            currentZ += Random.Range(6f, 12f); // spacing BEFORE spawn

            Vector3 spawnPos = new Vector3(
                (Random.value < 0.5f) ? 0.5f : 1.5f,
                (Random.value < 0.5f) ? 1f : 2.25f,
                currentZ
            );

            GameObject gate = Instantiate(gateChoicesPrefab, spawnPos, Quaternion.identity);
            RenderQuizChoices container = gate.GetComponent<RenderQuizChoices>();

            if (container != null)
            {
                container.Setup(
                    shuffledAnswers[i],
                    newCorrectIndex,
                    i
                );
            }
        }

        float finalGateZ = currentZ;

        // Check if player answer if not then -> take damage
        Vector3 checkTriggerPos = new Vector3(
            1f,
            1f,
            finalGateZ + 5f
        );

        Instantiate(checkIfanswerTrigger, checkTriggerPos, Quaternion.identity);

        // Spawn trigger for next quiz after some distance
        Vector3 triggerPos = new Vector3(
            1f,
            1f,
            finalGateZ + 15f
        ); Instantiate(newQuizTrigger, triggerPos, Quaternion.identity);

        Vector3 RandomizeGatePositions()
        {
            float[] Xoptions = { 0.5f, 1.5f };
            float x = Xoptions[Random.Range(0, Xoptions.Length)];

            float y = (Random.value < 0.3f) ? 1f : 1f;

            float z = distanceBetweenPlayer + distanceBetweenGates;

            Debug.Log($"Spawning gate at X: {x}, Y: {y}, Z: {z}");

            return new Vector3(x, y, z);
        }
    }
}