using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetThatSpins : Target
{
    protected override IEnumerator GotShotCoroutine()
    {
        originalRotation = transform.rotation;
        float timer = Time.time;
        float duration = Time.time + recoverDuration; 
        while (timer <= duration)
        {
            timer = Time.time;
            transform.Rotate(transform.up, 5);
            yield return null;
        }
        transform.rotation = originalRotation;
    }
}
