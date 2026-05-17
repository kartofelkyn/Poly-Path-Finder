using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This script is responsible for loading quiz data from a JSON string 
/// and storing it in a static list for use in the game. It defines a 
/// QuizWrapper class to match the expected JSON structure, which contains 
/// an array of Quiz objects. The LoadFromJSON method takes a JSON string, 
/// wraps it in an object with a "quizzes" field, and uses Unity's JsonUtility 
/// to parse it into a QuizWrapper instance. The quizzes are then stored in the 
/// AIQuizStorage static class for access throughout the game. This allows for 
/// dynamic loading of quiz data from external sources, such as an API response, 
/// enabling players to have a personalized quiz experience based on their uploaded content.
/// </summary>
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