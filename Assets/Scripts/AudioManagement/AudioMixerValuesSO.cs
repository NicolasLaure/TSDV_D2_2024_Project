using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New AudioMixerValues", menuName = "Audio/MixerValues", order = 0)]
public class AudioMixerValuesSO : ScriptableObject
{
    public float master;
    public float sfx;
    public float music;
}
