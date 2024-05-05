using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicWeapon : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    public override void FireWeapon()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
    }
}
