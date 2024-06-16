using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : FallingTarget
{
    private bool wasShotDown = false;
    public bool WasShotDown { get { return wasShotDown; } }
    public void SetDown()
    {
        if (transform.rotation == originalRotation)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);

        wasShotDown = false;
    }
    public void GetUp()
    {
        StartCoroutine(GetUpCoroutine());
    }
    protected override IEnumerator GotShotCoroutine()
    {
        wasShotDown = true;
        yield return FallCoroutine();
    }
}
