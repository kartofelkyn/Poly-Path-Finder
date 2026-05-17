using UnityEngine;
using TMPro;

/// <summary>
/// This script manages the popups in the game, such as score popups, streak popups, and live popups. 
/// It uses a singleton pattern to allow easy access from other scripts.
/// It provides methods to show different types of popups with customizable text and positioning. 
/// The score popup displays the points gained, the streak popup shows the current streak multiplier, 
/// and the live popup indicates changes in the player's lives. By centralizing the management of popups 
/// in this script, it simplifies the process of creating and displaying various popups throughout the game, 
/// ensuring a consistent and engaging user experience.
/// </summary>
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

    /// Shows a score popup with the specified value. The popup displays the points gained and is instantiated at the score popup container.
    public void ShowScore(int value)
    {

        GameObject popup = Instantiate(scorePopupPrefab, scorePopupContainer);

        ScorePopup sp = popup.GetComponent<ScorePopup>(); // Get the ScorePopup component from the instantiated popup to set the text

        sp.SetText("+" + value);
    }

    /// Shows a streak popup with the specified streak value. The popup displays the streak multiplier and is instantiated at the streak popup container.
    public void ShowStreak(int streak)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);

        streakPopupContainer.position = screenPos; // Set the position of the streak popup container to the player's screen position

        GameObject obj = Instantiate(streakPopupPrefab, streakPopupContainer); // Instantiate the streak popup prefab at the streak popup container

        RectTransform rect = obj.GetComponent<RectTransform>(); // Get the RectTransform component of the instantiated popup to set a random anchored position for the popup within a certain range, creating a dynamic effect for the streak popup

        rect.anchoredPosition = new Vector2( 
            Random.Range(-60f, 60f),
            Random.Range(60f, 120f)
        ); // Set a random anchored position for the popup within a certain range, creating a dynamic effect for the streak popup

        obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text =
            "Streak x" + streak;

        Destroy(obj, 1f);
    }

    /// Shows a live popup with the specified value. The popup displays the change in lives (positive or negative) and is instantiated at the live popup container.
    public void ShowLives(int value)
    {
        GameObject popup =
            Instantiate(livePopupPrefab, livePopupContainer); // Instantiate the live popup prefab at the live popup container

        popup.GetComponent<LivePopup>()
            .SetText(value.ToString("+#;-#;0"));
    }
}