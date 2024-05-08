using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected FireMode fireMode;
    private Coroutine fireCoroutine;
    [SerializeField] private int magazineSize;

    [Tooltip("Time in seconds for the weapon to be fired again")]
    [SerializeField] private float shootingCoolDown;
    private bool onCoolDown = false;
    private bool isReloading = false;
    private int currentMagazine;

    private bool isFiring = false;
    private Coroutine fullAutoCoroutine;

    public float LastShotTime { get; set; }
    protected void Start()
    {
        currentMagazine = magazineSize;
    }
    public void Shoot()
    {
        if (!CanShoot())
            return;

        if (fireCoroutine == null)
            fireCoroutine = StartCoroutine(fireMode.Fire(this));
    }
    public void StopShooting()
    {
        if (fullAutoCoroutine != null)
        {
            isFiring = false;
            StopCoroutine(fireCoroutine);
        }
    }
    public void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }
    public virtual void FireWeapon()
    {
        LastShotTime = Time.time;
        currentMagazine--;
    }
    public bool CanShoot()
    {
        return HasBullets() && !(isReloading) && !OnCoolDown();
    }
    public bool HasBullets()
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

    //private IEnumerator BurstAuto()
    //{
    //    isFiring = true;
    //    do
    //    {
    //        yield return StartCoroutine(Burst());
    //        yield return StartCoroutine(ShootCoolDown());
    //    } while (isFiring && CanShoot());
    //}
    private bool OnCoolDown()
    {
        return LastShotTime + shootingCoolDown > Time.time;
    }
    public IEnumerator ShootCoolDown()
    {
        onCoolDown = true;
        yield return new WaitForSeconds(shootingCoolDown);
        onCoolDown = false;
    }
}
