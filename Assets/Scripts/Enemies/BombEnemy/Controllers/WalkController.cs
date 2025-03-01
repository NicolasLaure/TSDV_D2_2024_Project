using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkController : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float distanceTreshold;

    private int currentWaypointIndex = 0;

    public void StartRoam()
    {
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void UpdateRoam()
    {
        if (navMeshAgent.remainingDistance > distanceTreshold) return;

        currentWaypointIndex = currentWaypointIndex + 1 < waypoints.Count ? currentWaypointIndex + 1 : 0;
        Debug.Log($"Change waypoint  {currentWaypointIndex}");
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}