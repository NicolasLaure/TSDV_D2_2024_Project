using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerConfigSO", menuName = "ScriptableObjects/Player", order = 0)]
public class PlayerConfigSO : ScriptableObject
{
    [Range(0.01f, 0.5f)]
    [SerializeField] public float lookSensitivity;

    private float min = 0.01f;
    private float max = 0.5f;
    public void SetSensitivity(float sens)
    {
        lookSensitivity = Mathf.Clamp(sens, min, max);
    }
}
