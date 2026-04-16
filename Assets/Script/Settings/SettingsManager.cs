using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [Header("User")]
    public string username;

    [Header("Progress")]
    public int highScore;

    [Header("Audio")]
    public float musicVolume;
    public float soundVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUsername(string value)
    {
        username = value;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;


        if (MusicManager.Instance != null)
            MusicManager.Instance.SetMusicVolume(value);
    }

    public void SetSoundVolume(float value)
    {
        soundVolume = value;

        if (MusicManager.Instance != null)
            MusicManager.Instance.SetSFXVolume(value);
    }

    public void SetHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void LoadSettings()
    {
        username = PlayerPrefs.GetString("Username", "Player");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetMusicVolume(musicVolume);
            MusicManager.Instance.SetSFXVolume(soundVolume);
        }
    }
    public void LoadSavedSettings()
    {
        username = PlayerPrefs.GetString("Username", "Player");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetMusicVolume(musicVolume);
            MusicManager.Instance.SetSFXVolume(soundVolume);
        }
    }
    public void ApplySettings()
    {
        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);

        PlayerPrefs.Save();

        Debug.Log("Settings Saved!");
    }
}