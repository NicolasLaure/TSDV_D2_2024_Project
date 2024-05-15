using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingTarget : Target
{
    bool canFall = true;
    private void Fall()
    {
        originalRotation = transform.rotation;
        transform.Rotate(transform.right, 90);
        canFall = false;
    }

    private void GetUp()
    {
        transform.rotation = originalRotation;
        canFall = true;
    }
    protected override IEnumerator GotShotCoroutine()
    {
        if (canFall)
        {
            Fall();
            yield return new WaitForSeconds(recoverDuration);
            GetUp();
        }
    }
}
