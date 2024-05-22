using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TrialTarget : FallingTarget
{
    [SerializeField] private float lifeDuration;
    [SerializeField] public bool isEnemy;

    private Coroutine turnOffCoroutine;
    public Action<bool> trialTargetShot;

    private void OnEnable()
    {
        StartCoroutine(GetUpCoroutine());
        turnOffCoroutine = StartCoroutine(TurnOffCoroutine());
    }
    protected override IEnumerator GotShotCoroutine()
    {
        GotShot();
        StopCoroutine(turnOffCoroutine);
        yield return FallCoroutine();
        gameObject.SetActive(false);
    }
    private IEnumerator TurnOffCoroutine()
    {
        yield return new WaitForSeconds(lifeDuration);
        yield return FallCoroutine();
        gameObject.SetActive(false);
    }
    private void GotShot()
    {
        //  trialTargetShot.Invoke(isEnemy);
    }
}
