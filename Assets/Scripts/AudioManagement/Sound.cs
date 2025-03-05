using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/Audio/Sound", order = 0)]
public class Sound : ScriptableObject
{
    public string name;
    public AudioClip audioClip;

    [SerializeField] private float volume;
    [SerializeField] private bool shouldLoop;
    [SerializeField] private float spatialBlend;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private AnimationCurve rollOffCurve;
    [SerializeField] private float spread;
    public AudioMixerGroup mixerGroup;

    private AudioSource _source;

    public void SetAudioSource(AudioSource newSource)
    {
        _source = newSource;

        _source.playOnAwake = false;
        _source.clip = audioClip;
        _source.volume = volume;
        _source.loop = shouldLoop;
        _source.spatialBlend = spatialBlend;
        _source.minDistance = minDistance;
        _source.maxDistance = maxDistance;
        _source.rolloffMode = AudioRolloffMode.Custom;
        _source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, rollOffCurve);
        _source.spread = spread;
        _source.outputAudioMixerGroup = mixerGroup;
    }
}