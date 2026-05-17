using UnityEngine;
using System.Collections;

/// <summary>
/// Manages footstep sounds for the player character.
/// This script uses a singleton pattern to allow easy access from other scripts. 
/// It plays footstep sounds at regular intervals while the player is moving, and it can 
/// fade between different footstep clips for variety. The footstep sounds are played 
/// through an AudioSource component, and the script allows for starting and stopping 
/// the footstep sounds as needed. The step duration and volume can be adjusted through 
/// the inspector, providing flexibility in how the footstep sounds are implemented in the game.
/// </summary>
 
public class FootstepManager : MonoBehaviour
{
    public static FootstepManager instance;

    [Header("Footstep Clips")]
    public AudioClip[] footstepClips; // Array of footstep audio clips to play randomly.

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Settings")]
    public float stepDuration = 120f;

    private Coroutine footstepRoutine;
    [Range(0f, 1f)]
    public float baseVolume = 0.2f;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartFootsteps()
    {
        if (footstepRoutine != null)
            StopCoroutine(footstepRoutine);

        footstepRoutine = StartCoroutine(FootstepLoop());
    }

    public void StopFootsteps()
    {
        if (footstepRoutine != null)
        {
            StopCoroutine(footstepRoutine);
            footstepRoutine = null;
        }

        audioSource.Stop();
    }
    IEnumerator FootstepLoop()
    {
        while (true)
        {
            PlayRandomFootstep();

            yield return new WaitForSeconds(stepDuration);
        }
    }

    void PlayRandomFootstep()
    {
        if (footstepClips.Length == 0) return;

        StartCoroutine(FadeSwitch());
    }

    IEnumerator FadeSwitch()
    {
        float fadeTime = 0.3f;

        // fade out
        float startVolume = audioSource.volume;

        // Runs over time (frame by frame)
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            // Gradually reduces volume from: startVolume to 0 over fadeTime duration
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
            yield return null;
        }

        // Stop the current clip
        audioSource.Stop();

        // switch clip
        audioSource.clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.Play();

        // fade in
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            // Gradually increases volume from: 0 to baseVolume over fadeTime duration
            audioSource.volume = Mathf.Lerp(0f, baseVolume, t / fadeTime);
            yield return null;
        }

        // Ensure volume is set to baseVolume at the end of the fade-in
        audioSource.volume = baseVolume;
    }
}