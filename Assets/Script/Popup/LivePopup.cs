using UnityEngine;
using TMPro;

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