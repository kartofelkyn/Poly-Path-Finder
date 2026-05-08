using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public int amount = 1;
    public AudioClip powerUpSound;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (type)
        {
            case PowerUpType.AddLife:
                GameManager.Instance.PowerupAddLife(amount);
                if (powerUpSound != null)
                    AudioManager.Instance.PlaySFX(powerUpSound);
                break;

            case PowerUpType.AddStreak:
                GameManager.Instance.PowerupAddStreak(amount);
                if (powerUpSound != null)
                    AudioManager.Instance.PlaySFX(powerUpSound);
                break;

            case PowerUpType.AddScore:
                GameManager.Instance.PowerupAddScore(amount);
                if (powerUpSound != null)
                    AudioManager.Instance.PlaySFX(powerUpSound);
                break;
        }

        Destroy(gameObject);
    }
}