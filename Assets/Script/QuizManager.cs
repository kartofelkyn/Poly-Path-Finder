// <copyright file="QuizManager.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;

/// <summary>
/// This script manages the quiz spawning and UI updates.
/// </summary>

public class QuizManager : MonoBehaviour
{
    public QuizData quizData;
    public GameObject gateChoicesPrefab;

    private float lastSpawnGateZ = 20f;
    public float distanceBetweenGates = 50f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnNextQuiz();
        }
    }

    public void SpawnNextQuiz()
    {
        // Pick a random question
        int randomIndex = Random.Range(0, quizData.quizzes.Length);
        Quiz selectedQuiz = quizData.quizzes[randomIndex];

        // Update the question UI
        QuestionUIManager.Instance.questionText.text = selectedQuiz.question;

        Vector3 spawnPos = new Vector3(2, 0, lastSpawnGateZ);
        GameObject newGate = Instantiate(gateChoicesPrefab, spawnPos, Quaternion.identity);
        RenderQuizChoices container = newGate.GetComponent<RenderQuizChoices>();

        if (container != null)
        {
            container.Setup(selectedQuiz.answers, selectedQuiz.correctIndex);
        }
    }
}
