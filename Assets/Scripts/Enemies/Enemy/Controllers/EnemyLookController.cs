using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FSM;
using UnityEngine;

public class EnemyLookController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;

    public void LookAtTarget()
    {
        Vector3 newRotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        if (newRotation.y < transform.rotation.eulerAngles.y)
            transform.Rotate(transform.up, -rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }
}