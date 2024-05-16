using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected FireMode fireMode;
    private Coroutine fireCoroutine;

    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private UnityEvent onReload;
    private bool isReloading = false;
    private int currentMagazine;

    private bool isFiring = false;
    private Coroutine fullAutoCoroutine;

    public float LastShotTime { get; set; }
    protected void Start()
    {
        currentMagazine = weaponSO.MagazineSize;
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
        onShoot.Invoke();
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
        onReload.Invoke();
        isReloading = true;
        yield return new WaitForSeconds(weaponSO.ReloadingDuration);
        isReloading = false;
        currentMagazine = weaponSO.MagazineSize;
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
        return LastShotTime + weaponSO.ShootingCoolDown > Time.time;
    }
    public IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(weaponSO.ShootingCoolDown);
    }
}
