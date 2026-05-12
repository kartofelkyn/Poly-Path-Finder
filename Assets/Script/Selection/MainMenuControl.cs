using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the main menu interactions, including starting the game,
/// navigating to settings and character selection panels, and quitting the game.
/// It also handles playing click sounds and menu music, as well as transitioning 
/// between different UI panels smoothly.
/// </summary>

public class MainMenuControl : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip menuMusic;
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;
    public GameObject characterSelectorPanel;

    void Start()
    {
        MusicManager.Instance.PlayMusic(menuMusic);
        SettingsManager.Instance.LoadSettings();
    }

    public void StartGame()
    {
        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);

        StartCoroutine(StartGameRoutine());
    }

    public void showSettings()
    {
        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);

        StartCoroutine(SwitchPanel(settingsPanel));
    }

    public void showCharacterSelector()
    {
        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);

        StartCoroutine(SwitchPanel(characterSelectorPanel));
    }

    public void backToMainMenu()
    {
        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);

        StartCoroutine(SwitchPanel(mainMenuPanel));
    }

    public void QuitGame()
    {
        if (clickSound != null)
            AudioManager.Instance.PlaySFX(clickSound);

        Application.Quit();
    }
    IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        SceneTransitionManager.Instance.BeginTransition("DifficultySelection");
    }
    IEnumerator SwitchPanel(GameObject panelToShow)
    {
        yield return new WaitForSeconds(0.35f);

        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        characterSelectorPanel.SetActive(false);

        panelToShow.SetActive(true);
    }
}