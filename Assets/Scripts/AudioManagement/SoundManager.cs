using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private List<AudioSource> _audioSources = new List<AudioSource>();

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            _audioSources.Add(gameObject.AddComponent<AudioSource>());
            sound.SetAudioSource(_audioSources[^1]);
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

    public void PlayRandomOnce()
    {
        AudioSource sourceToPlay = GetRandomSource();

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

        return _audioSources[index];
    }

    private AudioSource GetRandomSource()
    {
        return _audioSources[Random.Range(0, _audioSources.Count)];
    }
}