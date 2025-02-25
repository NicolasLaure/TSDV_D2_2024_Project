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

    public void CheckFrustum()
    {
        if (frustum.CheckPointInside(target.position))
            agent.ChangeStateToCombat();
    }
}