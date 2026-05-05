using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

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