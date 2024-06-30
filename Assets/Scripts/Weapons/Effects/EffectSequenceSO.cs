using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "ScriptableObjects/Weapons/Sequence", order = 0)]
public class EffectSequenceSO : ScriptableObject
{
    public List<WeaponEffectConfigSO> configs;
}
