using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string mixerVolumeParam;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerVolumeParam, Mathf.Log10(sliderValue) * 20);
    }
}