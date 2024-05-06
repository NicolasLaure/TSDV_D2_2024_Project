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

    private bool isFiring = false;
    private Coroutine fullAutoCoroutine;
    private void Start()
    {
        currentMagazine = magazineSize;
    }
    public void Shoot()
    {
        if (!canShoot)
            return;

        switch (firingMode)
        {
            case FiringModes.SEMI_AUTO:
                FireWeapon();
                StartCoroutine(ShootCoolDown());
                break;
            case FiringModes.BURST:
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
    protected abstract void FireWeapon();

    private IEnumerator Burst()
    {
        FireWeapon();
        yield return null;
        FireWeapon();
        yield return null;
        FireWeapon();
    }
    private IEnumerator FullAuto()
    {
        isFiring = true;
        while (isFiring)
        {
            FireWeapon();
            yield return null;
        }
    }

    private IEnumerator ShootCoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingCoolDown);
        canShoot = true;
    }
}
