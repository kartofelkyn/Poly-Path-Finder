using UnityEngine;

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

        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}