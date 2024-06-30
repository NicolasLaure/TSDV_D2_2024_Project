using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CreditGroup
{
    [SerializeField] public string Title;
    [SerializeField] public List<string> names = new List<string>();
    [SerializeField] public List<string> urls = new List<string>();
}
