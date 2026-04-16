using UnityEngine;
using UnityEngine.UI;

public class MusicSliderUI : MonoBehaviour
{
    public Slider musicSlider;

    void OnEnable()
    {
        Refresh();
        musicSlider.onValueChanged.AddListener(OnChanged);
    }

    void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(OnChanged);
    }

    void Refresh()
    {
        musicSlider.value = SettingsManager.Instance.musicVolume;
    }

    void OnChanged(float value)
    {
        Debug.Log($"Music Volume Changed: {value}");
        SettingsManager.Instance.SetMusicVolume(value);
    }
}