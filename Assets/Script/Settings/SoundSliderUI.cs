using UnityEngine;
using UnityEngine.UI;

public class SoundSliderUI : MonoBehaviour
{
    public Slider soundSlider;
    public AudioClip tickSound;

    void OnEnable()
    {
        Refresh();
        soundSlider.onValueChanged.AddListener(OnChanged);
    }

    void OnDisable()
    {
        soundSlider.onValueChanged.RemoveListener(OnChanged);
    }

    void Refresh()
    {
        soundSlider.value = SettingsManager.Instance.soundVolume;
    }

    void OnChanged(float value)
    {
        SettingsManager.Instance.SetSoundVolume(value);
        AudioManager.Instance.PlaySFX(tickSound);
    }
}