using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ThrowerModifier : Target
{
    [SerializeField] private int modifyValue;
    public Action<int> onModifierHit;
    protected override IEnumerator GotShotCoroutine()
    {
        onModifierHit?.Invoke(modifyValue);
        yield break;
    }
}
