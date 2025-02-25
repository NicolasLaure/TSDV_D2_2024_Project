using System.Collections;
using System.Collections.Generic;
using Enemy;
using FSM;
using UnityEngine;

public class SightController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private FrustumController frustum;
    [SerializeField] private EnemyAgent agent;

    public void CheckInSight(bool isSearching)
    {
        bool isTargetOnSight = frustum.CheckPointInside(target.position);

        if (isTargetOnSight && isSearching)
            agent.ChangeStateToCombat();
        else if (!isTargetOnSight && !isSearching)
            agent.ChangeStateToIdle();
    }
}