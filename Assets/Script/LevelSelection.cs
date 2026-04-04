using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public static string SelectedDifficulty = "Easy";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetEasy()
    {
        SelectedDifficulty = "Easy";
        StartGamePlay();
    }
    public void SetMedium()
    {
        SelectedDifficulty = "Medium";
        StartGamePlay();
    }
    public void SetHard()
    {        SelectedDifficulty = "Hard";
        StartGamePlay();
    }
    void StartGamePlay()
    {
        SceneManager.LoadScene(2);
    }
}
