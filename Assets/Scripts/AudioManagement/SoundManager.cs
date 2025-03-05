using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            audioSources.Add(gameObject.AddComponent<AudioSource>());
            sound.SetAudioSource(audioSources[^1]);
        }
    }

    public void Play(string soundName)
    {
        AudioSource sourceToPlay = FindAudioSource(soundName, sounds);

        if (sourceToPlay == null) return;

        sourceToPlay.Play();
    }

    public void PlayOnce(string soundName)
    {
        AudioSource sourceToPlay = FindAudioSource(soundName, sounds);

        if (sourceToPlay == null || sourceToPlay.isPlaying) return;

        sourceToPlay.Play();
    }

    public void Stop(string soundName)
    {
        AudioSource sourceToStop = FindAudioSource(soundName, sounds);
        if (sourceToStop == null) return;

        sourceToStop.Stop();
    }

    private AudioSource FindAudioSource(string soundName, Sound[] soundArray)
    {
        int index = -1;
        for (int i = 0; i < soundArray.Length; i++)
        {
            if (soundArray[i].name == soundName)
                index = i;
        }

        if (index < 0)
            return null;

        return audioSources[index];
    }
}