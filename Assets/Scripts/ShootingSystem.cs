using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private bool isHitScan;

    [Header("HitScan Attributes")]
    //[SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float maxHitDistance;
    private RaycastHit hit;
    private List<Vector3> hitPositions = new List<Vector3>();

    [Header("Physical Attributes")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        foreach (Vector3 position in hitPositions)
            Gizmos.DrawCube(position, new Vector3(0.2f, 0.2f, 0.2f));
    }
    public void Shoot()
    {
        if (isHitScan)
            HitScanShot();
        else
            PhysicalShot();
    }

    private void HitScanShot()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxHitDistance, layer))
        {
            Debug.Log($"HITSCAN SHOT Hit pos = {hit.point}");
            hitPositions.Add(hit.point);
        }
    }
    private void PhysicalShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
    }
}
