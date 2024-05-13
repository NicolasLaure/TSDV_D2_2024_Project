using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    Target target;

    private void Awake()
    {
        target = transform.parent.GetComponent<Target>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        target.shotReceived.Invoke();
    }
}
