using System.Collections;
using System.Collections.Generic;
using Enemies.BombEnemy;
using FSM;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WalkController : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float chaseSpeed;

    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float distanceTreshold;

    [SerializeField] private float distanceToDetonate;
    [SerializeField] private UnityEvent onDetonate;

    [SerializeField] private Transform target;
    private int _currentWaypointIndex = 0;

    public void StartRoam()
    {
        navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    public void UpdateRoam()
    {
        if (navMeshAgent.remainingDistance > distanceTreshold) return;

        _currentWaypointIndex = _currentWaypointIndex + 1 < waypoints.Count ? _currentWaypointIndex + 1 : 0;
        navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    public void UpdateTargetPosition()
    {
        navMeshAgent.SetDestination(target.position);

        if (navMeshAgent.remainingDistance < distanceToDetonate)
            onDetonate?.Invoke();
    }

    public void SetChaseSpeed()
    {
        navMeshAgent.speed = chaseSpeed;
    }

    public void SetRoamingSpeed()
    {
        navMeshAgent.speed = defaultSpeed;
    }

    public void Stop()
    {
        navMeshAgent.SetDestination(transform.position);
    }
}