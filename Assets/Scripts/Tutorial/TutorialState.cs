using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TutorialState : MonoBehaviour
{
    public Action onStateFinished;
    public void StartState()
    {
        StartCoroutine(StartStateCoroutine());
    }
    protected abstract IEnumerator StartStateCoroutine();

}
