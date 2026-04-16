using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [Header("UI References")]
    public CanvasGroup fadePanel;
    public Canvas loadingScreen;
    public Canvas mainPanel;

    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void BeginTransition(string sceneName)
    {
        fadePanel.alpha = 0;

        StartCoroutine(LoadWithFade(sceneName));
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadWithFade(sceneName));
    }

    IEnumerator LoadWithFade(string sceneName)
    {
        fadePanel.alpha = 0;

        yield return null;

        yield return StartCoroutine(Fade(1, 0, 0.2f));


        loadingScreen.enabled = true;
        mainPanel.enabled = false;

        yield return StartCoroutine(Fade(1, 0, fadeDuration));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0;

        while (timer < 3f || op.progress < 0.9f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Fade to black again
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        op.allowSceneActivation = true;

        while (!op.isDone)
            yield return null;

        // Fade into new scene
        yield return StartCoroutine(Fade(1, 0, fadeDuration));
    }

    IEnumerator Fade(float start, float end, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            fadePanel.alpha = Mathf.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        fadePanel.alpha = end;
    }
}