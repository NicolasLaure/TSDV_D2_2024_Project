using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicWeapon : Weapon
{
    [Header("Physical Attributes")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    public override void FireWeapon()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint);
        bullet.transform.parent = null;
        base.FireWeapon();
    }
}
