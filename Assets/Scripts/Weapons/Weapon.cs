using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected FiringModes firingMode;
    [SerializeField] private int magazineSize;

    [Tooltip("Time in seconds for the weapon to be fired again")]
    [SerializeField] private float shootingCoolDown;
    private bool onCoolDown = false;
    private bool isReloading = false;
    private int currentMagazine;

    [Header("Burst")]
    [SerializeField] private int bulletsPerBurst;
    [SerializeField] private float burstDuration;
    [SerializeField] private bool isBurstAuto;

   
    private bool isFiring = false;
    private Coroutine fullAutoCoroutine;
    protected void Start()
    {
        currentMagazine = magazineSize;
    }
    public void Shoot()
    {
        if (!CanShoot())
            return;

        switch (firingMode)
        {
            case FiringModes.SEMI_AUTO:
                FireWeapon();
                StartCoroutine(ShootCoolDown());
                break;
            case FiringModes.BURST:
                if (isBurstAuto)
                    fullAutoCoroutine = StartCoroutine(BurstAuto());
                else
                    StartCoroutine(Burst());

                StartCoroutine(ShootCoolDown());
                break;
            case FiringModes.FULL_AUTO:
                fullAutoCoroutine = StartCoroutine(FullAuto());
                break;
            default:
                break;
        }
    }
    public void StopShooting()
    {
        if (fullAutoCoroutine != null)
        {
            isFiring = false;
            StopCoroutine(fullAutoCoroutine);
            fullAutoCoroutine = null;
        }
    }
    public void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }
    protected virtual void FireWeapon()
    {
        currentMagazine--;
    }
    private bool CanShoot()
    {
        return HasBullets() && !(isReloading) && !(onCoolDown);
    }
    private bool HasBullets()
    {
        return currentMagazine > 0;
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(1);
        isReloading = false;
        currentMagazine = magazineSize;
    }

    private IEnumerator Burst()
    {
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            FireWeapon();
            if (!HasBullets())
                break;
            yield return new WaitForSeconds(burstDuration / bulletsPerBurst);
        }
    }
    private IEnumerator BurstAuto()
    {
        isFiring = true;
        do
        {
            yield return StartCoroutine(Burst());
            yield return StartCoroutine(ShootCoolDown());
        } while (isFiring && CanShoot());
    }
    private IEnumerator FullAuto()
    {
        isFiring = true;
        while (isFiring && CanShoot())
        {
            FireWeapon();
            yield return StartCoroutine(ShootCoolDown());
        }
    }

    private IEnumerator ShootCoolDown()
    {
        onCoolDown = true;
        yield return new WaitForSeconds(shootingCoolDown);
        onCoolDown = false;
    }
}
