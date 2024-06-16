using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : FallingTarget
{
    public void SetDown()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);
    }
    public void GetUp()
    {
        StartCoroutine(GetUpCoroutine());
    }
    protected override IEnumerator GotShotCoroutine()
    {
        yield return FallCoroutine();
    }
}
