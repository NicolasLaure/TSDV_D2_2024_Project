using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private string groupName;
    [SerializeField] private AudioMixerValuesSO soundVolumes;
    private Slider _slider;
    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        switch (groupName)
        {
            case "master":
                _slider.value = soundVolumes.master;
                break;
            case "sfx":
                _slider.value = soundVolumes.sfx;
                break;
            case "music":
                _slider.value = soundVolumes.music;
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
