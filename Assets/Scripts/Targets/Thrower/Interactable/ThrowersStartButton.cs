using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ThrowersStartButton : Target
{
    public Action onStartThrowing;
    protected override IEnumerator GotShotCoroutine()
    {
        onStartThrowing?.Invoke();
        yield break;
    }
}
