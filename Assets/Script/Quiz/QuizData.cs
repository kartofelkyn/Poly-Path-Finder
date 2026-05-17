using UnityEngine;

/// <summary>
/// This script defines the data structure for quizzes in the game, including the question, 
/// possible answers, the index of the correct answer, and the difficulty level. 
/// It also provides a method to filter quizzes based on their difficulty level, allowing 
/// for dynamic selection of quizzes during gameplay. By using a ScriptableObject, it allows 
/// for easy creation and management of quiz data within the Unity editor, making it simple to 
/// add new quizzes or modify existing ones without needing to change the code.
/// </summary>
 
public enum DifficultyLevel { Easy, Medium, Hard }

[System.Serializable]
public class Quiz
{
    public string question;
    public string[] answers;
    public int correctIndex;
    public DifficultyLevel difficulty; 
}

[CreateAssetMenu(fileName = "QuizDatabase", menuName = "Quiz/QuizData")]
public class QuizData : ScriptableObject
{
    public Quiz[] quizzes;

    public System.Collections.Generic.List<Quiz> GetQuizzesForDifficulty(string diffString)
    {
        System.Collections.Generic.List<Quiz> filtered = new System.Collections.Generic.List<Quiz>();
        
        foreach (var q in quizzes)
        {
            if (diffString == "Hard") 
                filtered.Add(q); // Hard gets everything
            else if (diffString == "Medium" && (q.difficulty == DifficultyLevel.Medium || q.difficulty == DifficultyLevel.Easy))
                filtered.Add(q); // Medium gets Easy + Medium
            else if (diffString == "Easy" && q.difficulty == DifficultyLevel.Easy)
                filtered.Add(q); // Easy gets Easy only
        }
        return filtered;
    }
}