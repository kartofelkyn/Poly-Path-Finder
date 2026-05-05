using UnityEngine;

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
