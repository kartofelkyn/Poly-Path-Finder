using UnityEngine;

/// <summary>
/// This script manages the gameplay sounds for the game. It uses a singleton pattern to allow easy
/// access from other scripts. It provides methods to play specific sounds for right answers, wrong answers, 
/// game end, and jumping. The sounds are played through an AudioManager if available, or directly at the camera's position 
/// if the AudioManager is not present. This allows for consistent sound effects throughout the game, and it centralizes 
/// the management of gameplay-related sounds in one place for easier maintenance and updates.
/// </summary>
/// 
public class GamePlaySound : MonoBehaviour
{
    public static GamePlaySound instance;

    public AudioClip rightAnswerSound;
    public AudioClip wrongAnswerSound;
    public AudioClip gameEndSound;
    public AudioClip jumpSound;


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

    public void AnswerRight()
    {
        PlaySound(rightAnswerSound);
    }

    public void AnswerWrong()
    {
        PlaySound(wrongAnswerSound);
    }
    public void GameEnd()
    {
        PlaySound(gameEndSound);
    }
    public void Jump()
    {
        PlaySound(jumpSound);
    }
    void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(clip);
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}