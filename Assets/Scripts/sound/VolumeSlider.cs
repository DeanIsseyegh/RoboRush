using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;
    [SerializeField] private string mixerVolumeParam;

    private void Start()
    {
        float savedSliderValue = PlayerPrefs.GetFloat(mixerVolumeParam, 1);
        slider.value = savedSliderValue;
        SetLevel(savedSliderValue);
    }

    public void SetLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat(mixerVolumeParam, sliderValue);
        float volume = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat(mixerVolumeParam, volume);
    }
}