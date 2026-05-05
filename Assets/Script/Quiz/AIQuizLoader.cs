using UnityEngine;
using System.Collections.Generic;

public class AIQuizLoader : MonoBehaviour
{
    [System.Serializable]
    public class QuizWrapper
    {
        public Quiz[] quizzes;
    }

    public void LoadFromJSON(string json)
    {
        string wrapped = "{ \"quizzes\": " + json + "}";

        QuizWrapper data = JsonUtility.FromJson<QuizWrapper>(wrapped);

        if (data == null || data.quizzes == null)
        {
            UnityEngine.Debug.LogError("Invalid AI JSON");
            return;
        }

        AIQuizStorage.aiQuizzes = new List<Quiz>(data.quizzes);

        UnityEngine.Debug.Log("AI Quiz Stored!");
    }
}