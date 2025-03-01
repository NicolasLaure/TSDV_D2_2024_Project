using System.Collections;
using System.Collections.Generic;
using Enemy;
using FSM;
using UnityEngine;
using UnityEngine.Events;

public class SightController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private FrustumController frustum;

    [SerializeField] private UnityEvent onSight;
    [SerializeField] private UnityEvent onOffSight;

    public void CheckInSight(bool isSearching)
    {
        bool isTargetOnSight = frustum.CheckPointInside(target.position);

        if (isTargetOnSight && isSearching)
            onSight?.Invoke();
        else if (!isTargetOnSight && !isSearching)
            onOffSight?.Invoke();
    }
}