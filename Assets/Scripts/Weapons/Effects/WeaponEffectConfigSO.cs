using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponEffect", menuName = "ScriptableObjects/Weapons/Effect", order = 0)]
public class WeaponEffectConfigSO : ScriptableObject
{
    public float delay;
    public AudioClip clip;
    public bool triggerParticles;
}
