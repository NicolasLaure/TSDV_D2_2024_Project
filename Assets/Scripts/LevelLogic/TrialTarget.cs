using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TrialTarget : MonoBehaviour
{
    [SerializeField] public bool isEnemy;
    private Target target;

    public Action<bool> trialTargetShot;
    private void Start()
    {
        target = GetComponent<Target>();
        target.shotReceived += GotShot;
    }

    private void GotShot()
    {
        trialTargetShot.Invoke(isEnemy);
    }
}
