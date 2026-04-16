using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public Slider slider;
    public string sceneName = "MainMenu";
    public FadingScript fadingScript;
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }
    bool hasStartedFade = false;
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Prevent automatic scene switch so we can control progress
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Unity progress goes 0 → 0.9 (NOT 1.0)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            // When fully loaded (almost done)
            if (operation.progress >= 0.9f && !hasStartedFade)
            {
                hasStartedFade = true;

                slider.value = 1f;
                
                yield return new WaitForSeconds(2f);

                // wait for fade to finish
                yield return fadingScript.FadeOut();

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
