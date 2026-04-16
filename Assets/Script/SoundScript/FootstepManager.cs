using UnityEngine;
using System.Collections;

public class FootstepManager : MonoBehaviour
{
    public static FootstepManager instance;

    [Header("Footstep Clips")]
    public AudioClip[] footstepClips;

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

        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
            yield return null;
        }

        audioSource.Stop();

        // switch clip
        audioSource.clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.Play();

        // fade in
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, baseVolume, t / fadeTime);
            yield return null;
        }

        audioSource.volume = baseVolume;
    }
}