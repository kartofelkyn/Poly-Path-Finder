using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
    public static string SelectedDifficulty = "Easy";
    public static bool useAI = false;
    public AudioClip clickSound;
    public void SetEasy()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        SelectedDifficulty = "Easy";
        useAI = false;

        StartCoroutine(SwitchScene("GamePlay"));
    }
    public void SetMedium()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        SelectedDifficulty = "Medium";
        useAI = false;

        StartCoroutine(SwitchScene("GamePlay"));
    }
    public void SetHard()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        SelectedDifficulty = "Hard";
        useAI = false;

        StartCoroutine(SwitchScene("GamePlay"));
    }

    public void goBack()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        StartCoroutine(SwitchScene("MainMenu"));
    }
    IEnumerator SwitchScene(string sceneName)
    {
        yield return new WaitForSeconds(0.27f);
        SceneTransitionManager.Instance.BeginTransition(sceneName);
    }
}
