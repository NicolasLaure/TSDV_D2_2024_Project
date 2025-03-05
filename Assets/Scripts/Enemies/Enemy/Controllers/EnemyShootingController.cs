using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingController : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private float shootingCoolDown;

    private float _lastShotTime;

    public void TryToShoot()
    {
        if (!CanShoot())
            return;

        _lastShotTime = Time.time;
        weapon.Shoot();
        weapon.StopShooting();
    }

    private bool CanShoot()
    {
        return Time.time - _lastShotTime > shootingCoolDown;
    }
}