using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnvironmentButton : Target
{
    [SerializeField] private UnityEvent OnButtonPressed;

    protected override IEnumerator GotShotCoroutine()
    {
        OnButtonPressed?.Invoke();
        yield break;
    }
}
