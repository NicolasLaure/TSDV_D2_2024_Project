using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New AudioMixerValues", menuName = "Audio/MixerValues", order = 0)]
public class AudioMixerValuesSO : ScriptableObject
{
    public float master = 0f;
    public float sfx = 0f;
    public float music = 0f;
}
