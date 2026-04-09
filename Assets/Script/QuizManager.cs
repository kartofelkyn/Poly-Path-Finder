// <copyright file="QuizManager.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;
using System.Collections.Generic; 
/// <summary>
/// This script manages the quiz spawning and UI updates.
/// </summary>

public class QuizManager : MonoBehaviour
{
    public QuizData quizData;
    public GameObject gateChoicesPrefab;

    private float lastSpawnGateZ = 20f;
    public float distanceBetweenGates = 50f;
    private int currentQuestionIndex = 0;
    private List<Quiz> shuffledQuizzes;

    void Start()
    {
        shuffledQuizzes = new List<Quiz>(quizData.quizzes);

        // Shuffle
        for (int i = 0; i < shuffledQuizzes.Count; i++)
        {
            int rand = Random.Range(i, shuffledQuizzes.Count);
            var temp = shuffledQuizzes[i];
            shuffledQuizzes[i] = shuffledQuizzes[rand];
            shuffledQuizzes[rand] = temp;
        }
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
        if (currentQuestionIndex >= 1)
        {
            Debug.Log("Quiz Finished!");
            GameManager.Instance.QuizComplete();
            return;
        }

        Quiz selectedQuiz = shuffledQuizzes[currentQuestionIndex];
        currentQuestionIndex++;

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
