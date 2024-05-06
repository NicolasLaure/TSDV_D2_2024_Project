using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected FiringModes firingMode;
    [SerializeField] private int magazineSize;
    private int currentMagazine;

    bool isFiring = false;
    Coroutine fullAutoCoroutine;
    private void Start()
    {
        currentMagazine = magazineSize;
    }
    public void Shoot()
    {
        switch (firingMode)
        {
            case FiringModes.SEMI_AUTO:
                FireWeapon();
                break;
            case FiringModes.BURST:
                StartCoroutine(Burst());
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
}
