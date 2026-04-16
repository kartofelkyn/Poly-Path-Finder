using UnityEngine;
using UnityEditor; // This was likely missing or the folder structure was wrong
using System;

[CustomEditor(typeof(QuizData))]
public class QuizImporterEditor : Editor
{
    private string jsonInput = "";
    private DifficultyLevel selectedDifficulty;

    public override void OnInspectorGUI()
    {
        // Draw the default inspector so you can still see the list
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
            // Unity's JsonUtility cannot deserialize a top-level array directly.
            // We wrap your pasted JSON so it looks like an object: { "quizzes": [...] }
            string wrappedJson = "{ \"quizzes\": " + jsonInput + "}";

            // We use a temporary wrapper to hold the data
            QuizDataWrapper importedData = JsonUtility.FromJson<QuizDataWrapper>(wrappedJson);

            if (importedData == null || importedData.quizzes == null)
            {
                Debug.LogError("Import failed: Resulting data is null. Check JSON format.");
                return;
            }

            QuizData data = (QuizData)target;

            // Apply the selected difficulty to all imported items
            foreach (var q in importedData.quizzes)
            {
                q.difficulty = selectedDifficulty;
            }

            // Merge with existing quizzes in the ScriptableObject
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

    // Add this small class at the bottom of your QuizImporterEditor.cs file
    // (outside the QuizImporterEditor class)
    [Serializable]
    public class QuizDataWrapper
    {
        public Quiz[] quizzes;
    }
}