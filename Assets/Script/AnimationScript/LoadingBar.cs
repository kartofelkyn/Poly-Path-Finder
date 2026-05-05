using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the loading bar UI and scene transition. 
/// It asynchronously loads a specified scene while updating a slider to reflect the loading progress. Once the loading is nearly complete, 
/// it triggers a fade-out effect before activating the new scene. This provides a smooth visual transition for the user during scene changes.
/// </summary>
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
