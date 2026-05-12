using UnityEngine;

/// <summary>
/// This script manages the audio for the game, specifically for sound effects (SFX).
/// It implements a singleton pattern to allow easy access from other scripts without 
/// needing direct references.
/// The AudioManager provides a method to play sound effects, which can be called 
/// from various parts of the game, such as button clicks or in-game events. 
/// The script ensures that only one instance of the AudioManager exists throughout 
/// the game, and it persists across scene loads to maintain audio continuity.
/// </summary>

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SFX")]
    public AudioSource sfxSource;

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

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}