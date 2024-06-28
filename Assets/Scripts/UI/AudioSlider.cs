using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private string groupName;
    [SerializeField] private AudioMixerValuesSO soundVolumes;
    private Slider slider;
    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        switch (groupName)
        {
            case "master":
                slider.value = soundVolumes.master;
                break;
            case "sfx":
                slider.value = soundVolumes.sfx;
                break;
            case "music":
                slider.value = soundVolumes.music;
                break;
        }
    }

    public void SetValues(float value)
    {
        switch (groupName)
        {
            case "master":
                soundVolumes.master = value;
                break;
            case "sfx":
                soundVolumes.sfx = value;
                break;
            case "music":
                soundVolumes.music = value;
                break;
        }
        AudioManager.instance.onVolumeChanged?.Invoke(soundVolumes);
    }
}
