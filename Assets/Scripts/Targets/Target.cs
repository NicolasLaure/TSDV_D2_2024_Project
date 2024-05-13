using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Target : MonoBehaviour
{
    [SerializeField] protected float recoverDuration;
    public Action shotReceived;

    protected Quaternion originalRotation;

    private void Awake()
    {
        shotReceived += OnShotReceived;
    }
  
    private void OnShotReceived()
    {
        StartCoroutine(GotShotCoroutine());
    }
    protected abstract IEnumerator GotShotCoroutine();
}
