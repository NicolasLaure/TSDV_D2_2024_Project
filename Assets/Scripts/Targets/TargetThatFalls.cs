using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetThatFalls : Target
{
    private void Fall()
    {
        originalRotation = transform.rotation;
        transform.Rotate(transform.forward, 90);
    }

    private void GetUp()
    {
        transform.rotation = originalRotation;
    }
    protected override IEnumerator GotShotCoroutine()
    {
        Fall();
        yield return new WaitForSeconds(recoverDuration);
        GetUp();
    }
}
