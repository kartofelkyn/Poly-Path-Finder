using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(QuizData))]
public class QuizImporterEditor : Editor
{
    private string jsonInput = "";
    private DifficultyLevel selectedDifficulty;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("JSON Bulk Importer", EditorStyles.boldLabel);

        selectedDifficulty = (DifficultyLevel)EditorGUILayout.EnumPopup("Difficulty to Assign:", selectedDifficulty);

        EditorGUILayout.LabelField("Paste JSON Array [...] here:");
        jsonInput = EditorGUILayout.TextArea(jsonInput, GUILayout.Height(100));

        if (GUILayout.Button("Import & Append Questions"))
        {
            ImportJSON();
        }
    }

    private void ImportJSON()
    {
        try
        {
            string wrappedJson = "{ \"quizzes\": " + jsonInput + "}";

            QuizDataWrapper importedData = JsonUtility.FromJson<QuizDataWrapper>(wrappedJson);

            if (importedData == null || importedData.quizzes == null)
            {
                Debug.LogError("Import failed: Resulting data is null. Check JSON format.");
                return;
            }

            QuizData data = (QuizData)target;

            foreach (var q in importedData.quizzes)
            {
                q.difficulty = selectedDifficulty;
            }

            int oldLength = (data.quizzes != null) ? data.quizzes.Length : 0;
            Array.Resize(ref data.quizzes, oldLength + importedData.quizzes.Length);
            Array.Copy(importedData.quizzes, 0, data.quizzes, oldLength, importedData.quizzes.Length);

            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            jsonInput = "";
            Debug.Log($"Successfully added {importedData.quizzes.Length} questions!");
        }
        catch (Exception e)
        {
            Debug.LogError("Import failed! Ensure you only pasted the [ ... ] part. Error: " + e.Message);
        }
    }


    [Serializable]
    public class QuizDataWrapper
    {
        public Quiz[] quizzes;
    }
}