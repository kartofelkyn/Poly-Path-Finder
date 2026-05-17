using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

/// <summary>
/// This script manages the uploading of quiz files to the server and loading the quiz data into the game. 
/// It provides a method to pick a file using the native file picker, upload the selected file to the specified 
/// API endpoint, and handle the response from the server. The response is expected to be in JSON format, which is 
/// then passed to the AIQuizLoader to load the quiz data into the game. This allows players to easily upload their 
/// own quiz files and have them integrated into the gameplay experience, providing a personalized and engaging way 
/// to play the game with custom content.
/// </summary>

public class UploadManager : MonoBehaviour
{
    public string apiUrl = "https://polypathfinder-quiz-ai-b7cwcrfgavanhncp.southeastasia-01.azurewebsites.net/upload";
    public Canvas loadingScreen;

    public AIQuizLoader loader; // assign in Inspector

    public void PickFile()
    {
        NativeFilePicker.PickFile((path) =>
        {
            if (path != null)
            {
                Debug.Log("Picked: " + path);
                StartCoroutine(UploadFile(path));
            }
        }, new string[] { "*/*" });
    }

    IEnumerator UploadFile(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path);

        loadingScreen.gameObject.SetActive(true);

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, fileName);

        UnityWebRequest request = UnityWebRequest.Post(apiUrl, form);

        yield return request.SendWebRequest();

        loadingScreen.gameObject.SetActive(false);

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Upload failed: " + request.error);
            Debug.LogError(request.downloadHandler.text);
            yield break;
        }

        string json = request.downloadHandler.text;
        Debug.Log("Response: " + json);

        if (loader == null)
        {
            Debug.LogError("AIQuizLoader not assigned!");
            yield break;
        }

        LevelSelection.useAI = true;
        loader.LoadFromJSON(json);

        SceneTransitionManager.Instance.BeginTransition("GamePlay");
    }
}