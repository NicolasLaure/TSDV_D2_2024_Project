using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanWeapon : Weapon
{
    [Header("HitScan Attributes")]
    [SerializeField] private LayerMask layer;
    [SerializeField] private float maxHitDistance;
    private RaycastHit _hit;
    private List<Vector3> _hitPositions = new List<Vector3>();

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        foreach (Vector3 position in _hitPositions)
            Gizmos.DrawCube(position, new Vector3(0.2f, 0.2f, 0.2f));
    }

    public override void FireWeapon()
    {
        base.FireWeapon();
        if (Physics.Raycast(pivot.position, BulletSpread(consecutiveShots), out _hit, maxHitDistance, layer))
        {
            if (decals != null)
                decals.SpawnBulletHole(_hit.transform, _hit.point, _hit.normal);

            _hitPositions.Add(_hit.point);

            if (_hit.transform.parent != null)
            {
                if (_hit.transform.parent.TryGetComponent(out Target hittedTargetInParent))
                {
                    hittedTargetInParent.shotReceived?.Invoke();
                }
                else if (_hit.transform.TryGetComponent(out Target hittedTarget))
                {
                    hittedTarget.shotReceived?.Invoke();
                }
            }
            else
            {
                if (_hit.transform.TryGetComponent(out Target hittedTarget))
                {
                    hittedTarget.shotReceived?.Invoke();
                }
            }

            if (_hit.transform.TryGetComponent(out IHittable hittedCollider))
            {
                Debug.Log("EnemyHit");
                hittedCollider.GetHit(WeaponSO.BulletDamage);
            }
        }
    }
}