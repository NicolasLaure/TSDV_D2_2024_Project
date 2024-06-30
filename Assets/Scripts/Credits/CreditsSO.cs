using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Credits", menuName = "ScriptableObjects/Credits/CreditsSO", order = 0)]
public class CreditsSO : ScriptableObject
{
    [SerializeField] public List<CreditGroup> groups = new List<CreditGroup>();
}
