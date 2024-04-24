using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponSO", menuName = "ScriptableObjects/Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] public string weaponName { get { return weaponName; } private set { } }
    [SerializeField] public GameObject weaponPrefab;
}
