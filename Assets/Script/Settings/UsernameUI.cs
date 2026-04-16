using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsernameUI : MonoBehaviour
{
    public TMP_InputField inputUsername;

    void Start()
    {
        inputUsername.text = SettingsManager.Instance.username;
        inputUsername.onEndEdit.AddListener(OnChanged);
    }

    void OnChanged(string value)
    {
        SettingsManager.Instance.SetUsername(value);
    }
}