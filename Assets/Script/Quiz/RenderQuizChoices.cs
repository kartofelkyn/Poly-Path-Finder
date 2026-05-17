// <copyright file="RenderQuizChoices.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;

/// <summary>
/// This script is responsible for rendering the quiz choices on the lanes and handling the player's answer when they collide with a lane.
/// </summary>

public class RenderQuizChoices : MonoBehaviour
{
    public TextMeshPro laneText;
    private int correctIndex;

    public int index = 0;
    public void Setup(string answer, int correctIdx, int myIndex)
    {
        laneText.text = answer;
        correctIndex = correctIdx;

        // store which answer this gate represents
        this.index = myIndex;
    }

    public void OnLaneHit()
    {
        if (QuizManager.Instance.questionAnswered) return;
        //Debug.Log($"Lane hit: {QuizManager.Instance.questionAnswered}");

        QuizManager.Instance.questionAnswered = true;

        //Debug.Log($"Lane hit: {QuizManager.Instance.questionAnswered}");
        if (index == correctIndex)
        {
            GameManager.Instance.AddScore(100);
            GamePlaySound.instance.AnswerRight();
            QuizManager.Instance.checkIfAnswerCalled = true;
            QuestionUIManager.Instance.questionText.text =
            "You got it right!" +
            "\nCorrect Answer: " + QuizManager.Instance.currentCorrectAnswer;
            
            //Debug.Log("Correct Answer!");
        }
        else
        {
            GameManager.Instance.TakeDamage(1);
            GamePlaySound.instance.AnswerWrong();
            QuizManager.Instance.checkIfAnswerCalled = true;
            QuestionUIManager.Instance.questionText.text =
            "You got it wrong!" +
            "\nCorrect Answer: " + QuizManager.Instance.currentCorrectAnswer;
            //Debug.Log("Wrong Answer!");
        }
    }
}
