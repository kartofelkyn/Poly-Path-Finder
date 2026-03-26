// <copyright file="QuizData.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>
using UnityEngine;

/// <summary>
/// This script defines the data structure for the game built-in quiz questions and answers. 
/// </summary>

[System.Serializable]
public class Quiz
{
    public string question;
    public string[] answers;
    public int correctIndex;
}

[CreateAssetMenu(fileName = "QuizDatabase", menuName = "Quiz/QuizData")]
public class QuizData : ScriptableObject
{
    public Quiz[] quizzes;
}