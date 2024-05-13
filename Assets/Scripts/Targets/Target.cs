using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    [SerializeField] float recoverDuration;
    private MeshRenderer mesh;
    public Action shotReceived;
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
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
        mesh.enabled = false;
        yield return new WaitForSeconds(recoverDuration);
        mesh.enabled = true;
    }
}
