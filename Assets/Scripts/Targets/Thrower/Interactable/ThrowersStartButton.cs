using System;
using System.Collections;

public class ThrowersStartButton : Target
{
    public Action onStartThrowing;
    protected override IEnumerator GotShotCoroutine()
    {
        onStartThrowing?.Invoke();
        yield break;
    }
}
