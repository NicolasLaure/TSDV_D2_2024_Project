using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : FallingTarget
{
    private bool _wasShotDown = false;
    public bool WasShotDown { get { return _wasShotDown; } }
    public void SetDown()
    {
        if (transform.rotation == originalRotation)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);

        _wasShotDown = false;
    }
    public void GetUp()
    {
        StartCoroutine(GetUpCoroutine());
    }
    protected override IEnumerator GotShotCoroutine()
    {
        _wasShotDown = true;
        yield return FallCoroutine();
    }
}
