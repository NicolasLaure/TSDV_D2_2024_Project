using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected FiringModes firingMode;
    [SerializeField] private int magazineSize;

    [Tooltip("Time in seconds for the weapon to be fired again")]
    [SerializeField] private float shootingCoolDown;
    private bool canShoot = true;
    private int currentMagazine;

    [Header("Burst")]
    [SerializeField] private int bulletsPerBurst;
    [SerializeField] private float burstDuration;
    [SerializeField] private bool isBurstAuto;

    [Header("FullAuto")]
    [SerializeField] private float timeBetweenRounds;
    private bool isFiring = false;
    private Coroutine fullAutoCoroutine;
    protected void Start()
    {
        currentMagazine = magazineSize;
    }
    public void Shoot()
    {
        if (!canShoot || !HasBullets())
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
    protected virtual void FireWeapon()
    {
        currentMagazine--;
    }

    private bool HasBullets()
    {
        return currentMagazine > 0;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1);
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
            yield return new WaitForSeconds(timeBetweenRounds);
        } while (isFiring && HasBullets());
    }
    private IEnumerator FullAuto()
    {
        isFiring = true;
        while (isFiring && HasBullets())
        {
            FireWeapon();
            yield return new WaitForSeconds(timeBetweenRounds);
        }
    }

    private IEnumerator ShootCoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingCoolDown);
        canShoot = true;
    }
}
