using UnityEngine;
using TMPro;

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