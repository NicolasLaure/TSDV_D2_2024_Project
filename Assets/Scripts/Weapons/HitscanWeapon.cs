using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanWeapon : Weapon
{
    [Header("HitScan Attributes")]
    [SerializeField] private LayerMask layer;
    [SerializeField] private float maxHitDistance;
    private RaycastHit hit;
    private List<Vector3> hitPositions = new List<Vector3>();

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        foreach (Vector3 position in hitPositions)
            Gizmos.DrawCube(position, new Vector3(0.2f, 0.2f, 0.2f));
    }

    public override void FireWeapon()
    {
        base.FireWeapon();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxHitDistance, layer))
        {
            Debug.Log($"Hit: {hit.transform.name}");
            
            if (decals != null)
                decals.SpawnBulletHole(hit.transform, hit.point, hit.normal);

            hitPositions.Add(hit.point);

            if (hit.transform.parent.TryGetComponent(out Target hittedTarget))
            {
                hittedTarget.shotReceived?.Invoke();
            }

            if (hit.transform.TryGetComponent(out IHittable hittedCollider))
            {
                Debug.Log("EnemyHit");
                hittedCollider.GetHit(WeaponSO.BulletDamage);
            }
        }
    }
}