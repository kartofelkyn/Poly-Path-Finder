using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages scene transitions with fade effects 
/// and loading screens. It provides a smooth visual experience 
/// when switching between scenes, especially when loading the 
/// main gameplay scene. The script handles both fading in and 
/// out of a canvas group for transitions and displaying a loading 
/// screen with fun facts while the new scene is being loaded asynchronously.
/// Depending on the target scene, it either shows a loading 
/// screen or performs a fade transition. The script is designed 
/// to be a singleton, allowing easy access from other parts of the code 
/// to initiate scene transitions without needing direct references to 
/// the transition manager. This centralizes the scene transition logic 
/// and ensures consistency across the game.
/// </summary> 

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [Header("UI References")]
    public CanvasGroup fadePanel;
    public Canvas loadingScreen;
    public Canvas mainPanel;

    public float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void BeginTransition(string sceneName)
    {
        StartCoroutine(LoadWithFade(sceneName));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadWithFade(sceneName));
    }

    IEnumerator LoadWithFade(string sceneName)
    {
        if (sceneName == "GamePlay")
        {
            yield return null;

            loadingScreen.gameObject.SetActive(true);
            mainPanel.gameObject.SetActive(false);
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;
            yield return new WaitForSeconds(1f);

            while (op.progress < 0.9f)
            {
                yield return null;
            }
            op.allowSceneActivation = true;

            while (!op.isDone)
            {
                yield return null;
            }
            loadingScreen.gameObject.SetActive(false);

        }
        else
        {
            fadePanel.gameObject.SetActive(true);
            yield return null;

            yield return StartCoroutine(Fade(0, 1, fadeDuration));

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;

            yield return new WaitForSeconds(0.2f);

            while (op.progress < 0.9f)
            {
                yield return null;
            }

            op.allowSceneActivation = true;

            while (!op.isDone)
            {
                yield return null;
            }

            yield return StartCoroutine(Fade(1, 0, fadeDuration));
        }


    }

    IEnumerator Fade(float start, float end, float duration)
    {
        float time = 0f;

        fadePanel.alpha = start;

        while (time < duration)
        {
            time += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(start, end, time / duration);
            yield return null;
        }

        fadePanel.alpha = end;
    }
}