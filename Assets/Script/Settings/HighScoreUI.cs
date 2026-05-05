using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = $"{SettingsManager.Instance.highScore}";
    }
}