using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : FallingTarget
{
    private void OnEnable()
    {
        originalRotation = transform.rotation;
    }
    protected override IEnumerator GotShotCoroutine()
    {
        yield return FallCoroutine();
        ClearDecals();
        gameObject.SetActive(false);
    }
}
