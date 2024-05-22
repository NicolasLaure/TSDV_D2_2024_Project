using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TrialTarget : FallingTarget
{
    [SerializeField] private float lifeDuration;
    [SerializeField] public bool isEnemy;

    [SerializeField] private Material enemyMat;
    [SerializeField] private Material hostageMat;

    private Coroutine turnOffCoroutine;
    public Action<bool> onTrialTargetShot;

    private void OnEnable()
    {
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        if (isEnemy)
            mesh.material = enemyMat;
        else
            mesh.material = hostageMat;

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
        onTrialTargetShot.Invoke(isEnemy);
    }
}
