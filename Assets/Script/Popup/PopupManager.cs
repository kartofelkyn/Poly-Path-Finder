using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    public GameObject scorePopupPrefab;
    public Transform scorePopupContainer;

    public GameObject streakPopupPrefab;
    public Transform streakPopupContainer;

    public GameObject livePopupPrefab;
    public Transform livePopupContainer;

    public Transform player;
    public Camera cam;
    void Awake()
    {
        Instance = this;
    }

    public void ShowScore(int value)
    {

        GameObject popup = Instantiate(scorePopupPrefab, scorePopupContainer);

        ScorePopup sp = popup.GetComponent<ScorePopup>();

        sp.SetText("+" + value);
    }

    public void ShowStreak(int streak)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);

        streakPopupContainer.position = screenPos;

        GameObject obj = Instantiate(streakPopupPrefab, streakPopupContainer);

        RectTransform rect = obj.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(
            Random.Range(-60f, 60f),
            Random.Range(60f, 120f)
        );

        obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text =
            "Streak x" + streak;

        Destroy(obj, 1f);
    }
    public void ShowLives(int value)
    {
        GameObject popup =
            Instantiate(livePopupPrefab, livePopupContainer);

        popup.GetComponent<LivePopup>()
            .SetText(value.ToString("+#;-#;0"));
    }
}