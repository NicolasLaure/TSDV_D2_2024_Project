using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    [SerializeField] float recoverDuration;
    private Collider targetCollider;
    public Action shotReceived;
    private void Awake()
    {
        targetCollider = GetComponent<Collider>();
        shotReceived += OnShotReceived;
    }
    private void OnCollisionEnter(Collision collision)
    {
        shotReceived.Invoke();
    }
    private void OnShotReceived()
    {
        StartCoroutine(GotShotCoroutine());
    }
    private IEnumerator GotShotCoroutine()
    {
        targetCollider.enabled = false;
        yield return new WaitForSeconds(recoverDuration);
        targetCollider.enabled = true;
    }
}
