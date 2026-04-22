using UnityEngine;

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
