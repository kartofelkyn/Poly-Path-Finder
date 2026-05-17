using UnityEngine;

/// <summary>
/// This script checks if the player has answered a quiz question correctly or not.
/// When the player enters the trigger area, it checks if the checkIfAnswerCalled flag 
/// in the QuizManager is false, indicating that the player has not answered the question yet.
/// If the flag is false, it means the player has answered incorrectly, so it applies 
/// damage to the player and plays a sound effect for a wrong answer.
/// If the flag is true, it means the player has answered correctly, so it simply resets 
/// the flags in the QuizManager without applying damage. This script ensures that the player 
/// is penalized for incorrect answers while allowing them to proceed if they have answered correctly.
/// </summary>

public class CheckIfAnswer : MonoBehaviour
{
    bool flag = false;
    void SetUp()
    {
        flag = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool checker = QuizManager.Instance.checkIfAnswerCalled;
            if (checker == false && !flag)
            {
                flag = true;
                GameManager.Instance.TakeDamage(1);
                GamePlaySound.instance.AnswerWrong();
                QuizManager.Instance.checkIfAnswerCalled = false;
                QuizManager.Instance.questionAnswered = false;
            }
            else
            {
                flag = true;
                QuizManager.Instance.checkIfAnswerCalled = false;
                QuizManager.Instance.questionAnswered = false;
                return;
            }
        }
    }
}
