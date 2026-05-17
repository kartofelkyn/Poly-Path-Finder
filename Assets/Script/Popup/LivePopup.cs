using UnityEngine;
using TMPro;


/// <summary>
/// This script manages the live popups in the game, which display changes in the player's lives.
/// The live popup shows a positive value when the player gains a life and a negative value when 
/// the player loses a life. The popup moves upwards and fades out over time, creating a visual 
/// effect that indicates the change in lives. By centralizing the management of live popups in this 
/// script, it simplifies the process of creating and displaying these popups throughout the game, 
/// ensuring a consistent and engaging user experience.
/// </summary>

public class LivePopup : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float moveSpeed = 50f;
    public float fadeSpeed = 1f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0.5f;
    }

    public void SetText(string value)
    {
        text.text = value;
    }

    void Update()
    {
        transform.position -= Vector3.up * moveSpeed * Time.deltaTime;

        canvasGroup.alpha -= fadeSpeed * Time.deltaTime;

        // Destroy when invisible
        if (canvasGroup.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}