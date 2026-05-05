using UnityEngine;
using UnityEngine.SceneManagement;

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

        SceneTransitionManager.Instance.BeginTransition("GamePlay");
    }
    public void SetMedium()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        SelectedDifficulty = "Medium";
        useAI = false;

        SceneTransitionManager.Instance.BeginTransition("GamePlay");
    }
    public void SetHard()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        SelectedDifficulty = "Hard";
        useAI = false;

        SceneTransitionManager.Instance.BeginTransition("GamePlay");
    }

    public void goBack()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        SceneTransitionManager.Instance.BeginTransition("MainMenu");
    }
}
