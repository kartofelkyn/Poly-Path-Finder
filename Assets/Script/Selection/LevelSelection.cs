using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This script manages the level selection process in the game. 
/// It allows players to choose between different difficulty levels 
/// (Easy, Medium, Hard) and also provides an option to go back to
/// the main menu. When a difficulty level is selected, it sets 
/// the appropriate static variables to indicate the chosen difficulty 
/// and whether AI should be used. It also plays a click sound for feedback 
/// and initiates a scene transition to the gameplay scene after a short delay.
/// The script uses a coroutine to handle the scene transition, allowing 
/// for a smooth experience when switching scenes. The static variables set 
/// in this script can be accessed by other parts of the game to adjust gameplay 
/// mechanics based on the selected difficulty level.
/// </summary>

public class LevelSelection : MonoBehaviour
{
    public static string SelectedDifficulty = "Easy";
    public static bool useAI = false;
    public AudioClip clickSound;
    public void SetEasy()
    {
        if (clickSound != null)
            //Note: Need palitang ng AudioManager.Instance.PlaySFX(clickSound); pag nagawa na
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
