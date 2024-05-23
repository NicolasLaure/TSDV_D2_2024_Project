using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerConfigSO", menuName = "ScriptableObjects/Player", order = 0)]
public class PlayerConfigSO : ScriptableObject
{
    [Range(0, 1)]
    [SerializeField] public float lookSensitivity;
}
