using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerValuesSO defaultVolumes;
    [SerializeField] private AudioMixerValuesSO currentVolumes;
    public Action<AudioMixerValuesSO> onVolumeChanged;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        onVolumeChanged += OnVolumeChanged;
        SetDefaultVolumes();
    }

    private void OnDestroy()
    {
        onVolumeChanged -= OnVolumeChanged;
    }

    private void OnVolumeChanged(AudioMixerValuesSO values)
    {
        mixer.SetFloat("Master", linearToDecibel(values.master));
        mixer.SetFloat("Sfx", linearToDecibel(values.sfx));
        mixer.SetFloat("Music", linearToDecibel(values.music));
    }

    public void SetDefaultVolumes()
    {
        currentVolumes.master = defaultVolumes.master;
        currentVolumes.sfx = defaultVolumes.sfx;
        currentVolumes.music = defaultVolumes.music;

        onVolumeChanged?.Invoke(currentVolumes);
    }

    /// <summary>
    /// Returns a decibel in the proper range [-144Db, 15Db] clamping input between 0 and 2
    /// </summary>
    private float linearToDecibel(float input)
    {
        input = Mathf.Clamp(input, 0, 2);

        if (input == 0)
            return -144f;

        return Mathf.Log10(input) * 50;
    }
}
