using UnityEngine;

/// <summary>
/// This script is responsible for triggering the next quiz when the player enters the trigger area.
/// When the player enters the trigger area, it calls the SpawnNextQuiz method from the QuizManager
/// to spawn the next quiz and then destroys the trigger game object to prevent multiple triggers.
/// </summary>

public class NewQuizTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuizManager.Instance.SpawnNextQuiz();
            Destroy(gameObject);
        }
    }
}
