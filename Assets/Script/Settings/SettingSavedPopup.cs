using UnityEngine;
using TMPro;

public class SettingsPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CanvasGroup canvasGroup;

    public float moveSpeed = 30f;
    public float fadeSpeed = 2f;
    private bool isActive = false;

    void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void Activate(string value)
    {
        text.text = value;
        canvasGroup.alpha = 1f;
        isActive = true;
    }

    void Update()
    {
        if (!isActive) return;

        transform.position += Vector3.up * moveSpeed * Time.deltaTime * 0.8f;

        canvasGroup.alpha -= fadeSpeed * Time.deltaTime;

        if (canvasGroup.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}