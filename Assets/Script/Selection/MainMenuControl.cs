using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip menuMusic;
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;
    void Start()
    {
        MusicManager.Instance.PlayMusic(menuMusic);
        SettingsManager.Instance.LoadSettings();
    }

    public void StartGame()
    {
        Debug.Log("Start Game Clicked");

        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);

        StartCoroutine(StartGameRoutine());
    }
    public void showSettings()
    {
        Debug.Log("Settings Clicked");

        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        
    }
    public void backToMainMenu()
    {
        Debug.Log("Back to Main Menu Clicked");

        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);
        
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        SceneTransitionManager.Instance.BeginTransition("DifficultySelection");
    }
}