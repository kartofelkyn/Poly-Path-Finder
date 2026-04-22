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

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            if (operation.progress >= 0.9f && !hasStartedFade)
            {
                hasStartedFade = true;

                slider.value = 1f;
                
                yield return new WaitForSeconds(2f);

                yield return fadingScript.FadeOut();

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
