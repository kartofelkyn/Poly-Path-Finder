using UnityEngine;
using TMPro;

/// <summary>
/// This script manages the score popups in the game, which display points gained or lost.
/// The score popup shows a positive value when the player gains points and a negative value when 
/// the player loses points. The popup moves upwards and fades out over time, creating a visual 
/// effect that indicates the change in score. By centralizing the management of score popups in this 
/// script, it simplifies the process of creating and displaying these popups throughout the game, 
/// ensuring a consistent and engaging user experience.
/// </summary>
public class ScorePopup : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float moveSpeed = 50f;
    public float fadeSpeed = 2f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetText(string value)
    {
        text.text = value;
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        canvasGroup.alpha -= fadeSpeed * Time.deltaTime;

        if (canvasGroup.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}