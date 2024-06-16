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
        originalRotation = transform.rotation;
    }
  
    private void OnShotReceived()
    {
        StartCoroutine(GotShotCoroutine());
    }
    protected abstract IEnumerator GotShotCoroutine();

    public void ClearDecals()
    {
        foreach (Collider collider in transform.GetComponentsInChildren<Collider>())
        {
            foreach (Transform decal in collider.transform)
            {
                if (decal.CompareTag("Decal"))
                    FindObjectOfType<DecalsHandler>().RemoveDecal(decal.gameObject);
            }
        }
    }
}
