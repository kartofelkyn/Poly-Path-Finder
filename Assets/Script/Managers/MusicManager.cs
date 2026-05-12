using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This script manages the audio for the game, specifically for music.
/// It implements a singleton pattern to allow easy access from other scripts without 
/// needing direct references.
/// The MusicManager provides methods to play music and control its volume, ensuring 
/// a consistent audio experience throughout the game.
/// </summary>
/// 
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio")]
    public AudioMixer mixer;
    public AudioSource musicSource;
    private const float baseMusicMultiplier = 0.5f;
    private const float baseSFXMultiplier = 0.2f;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SetMusicVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        float adjusted = value * baseMusicMultiplier;
        adjusted = Mathf.Clamp(adjusted, 0.0001f, 1f);

        float dB = Mathf.Log10(adjusted) * 20f;
        mixer.SetFloat("MusicVolume", dB);
    }

    public void SetSFXVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        float adjusted = value * baseSFXMultiplier;
        adjusted = Mathf.Clamp(adjusted, 0.0001f, 1f);

        float dB = Mathf.Log10(adjusted) * 20f;
        mixer.SetFloat("SFXVolume", dB);
    }
}