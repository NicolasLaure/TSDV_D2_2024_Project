using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private bool isHitScan;

    [Header("HitScan Attributes")]
    //[SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private RaycastHit hit;
    private List<Vector3> hitPositions = new List<Vector3>();

    [Header("Physic Attributes")]
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
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, layer);

        hitPositions.Add(hit.point);
    }
    private void PhysicalShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
    }
}
