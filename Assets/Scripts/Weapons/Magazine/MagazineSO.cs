using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Magazine", menuName = "ScriptableObjects/Weapons/Magazine", order = 0)]
public class MagazineSO : ScriptableObject
{
    [SerializeField] private int size = 0;
    [HideInInspector] public int Size { get { return size; } }
}
