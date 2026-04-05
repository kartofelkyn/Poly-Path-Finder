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
    public TextMeshPro[] laneTexts;
    private int correctIndex;
    private bool hasBeenAnswered = false;

    public void Setup(string[] answers, int correctIdx)
    {
        correctIndex = correctIdx;
        for (int i = 0; i < laneTexts.Length; i++)
        {
            laneTexts[i].text = answers[i];
        }
    }

    public void OnLaneHit(int index)
    {
        if (hasBeenAnswered) return;
        hasBeenAnswered = true;

        if (index == correctIndex)
        {
            GameManager.Instance.AddScore(100);
            Debug.Log("Correct Answer!");
        }
        else
        {
            GameManager.Instance.TakeDamage(1);
            Debug.Log("Wrong Answer!");
        }

    }
}
