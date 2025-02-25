using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class EnemyLookController : MonoBehaviour
{
    [SerializeField] private Transform target;

    public void LookAtTarget()
    {
        Vector3 newRotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Euler(newRotation);
    }
}