using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    [SerializeField] private Target target;

    private void Awake()
    {
        if (target == null)
            target = transform.parent.GetComponent<Target>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        target.shotReceived?.Invoke();
    }
}
