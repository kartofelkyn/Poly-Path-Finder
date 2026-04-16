using UnityEngine;

public class GamePlaySound : MonoBehaviour
{
    public static GamePlaySound instance;

    public AudioClip rightAnswerSound;
    public AudioClip wrongAnswerSound;
    public AudioClip gameEndSound;


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

    void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}