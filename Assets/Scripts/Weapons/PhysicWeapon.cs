using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicWeapon : Weapon
{
    [Header("Physical Attributes")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    protected override void FireWeapon()
    {
        base.FireWeapon();
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint);
        bullet.transform.parent = null;
    }
}
