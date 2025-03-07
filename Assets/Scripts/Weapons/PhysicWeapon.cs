using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicWeapon : Weapon
{
    [Header("Physical Attributes")]
    [SerializeField] private BulletConfigSO bulletConfig;
    [SerializeField] private Transform shootingPoint;

    public override void FireWeapon()
    {
        GameObject bullet = BulletFactory.CreateBullet(bulletConfig, decals, this.shootingPoint);
        bullet.transform.parent = null;
        bullet.GetComponent<Bullet>().damage = WeaponSO.BulletDamage;
        base.FireWeapon();
    }
}