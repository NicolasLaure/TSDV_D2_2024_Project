using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrialButton : Target
{
    public UnityEvent onButtonHit;

    protected override IEnumerator GotShotCoroutine()
    {
        onButtonHit?.Invoke();
        yield break;
    }
}
