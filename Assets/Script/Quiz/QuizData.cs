using UnityEngine;

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