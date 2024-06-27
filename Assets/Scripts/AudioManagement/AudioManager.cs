using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixer mixer;

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

    /// <summary>
    /// Returns a decibel in the proper range [-144Db, 20Db] clamping input between 0 and 100
    /// </summary>
    private float linearToDecibel(float input)
    {
        input = Mathf.Clamp(input, 0, 100);

        if (input == 0)
            return -144f;

        return Mathf.Log10(input) * 10;
    }
}
