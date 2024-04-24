using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform holdingPoint;
    [SerializeField] private WeaponSO currentWeaponSO = null;
    private GameObject currentWeapon = null;
    private bool isInCombatMode = true;

    private void Start()
    {
        if (currentWeaponSO != null)
            currentWeapon = Instantiate(currentWeaponSO.weaponPrefab, holdingPoint);
    }
    public void OnWeaponGrab(WeaponSO newWeapon)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(currentWeaponSO.weaponPrefab, holdingPoint);

    }

    public void ShootWeapon()
    {
        if (isInCombatMode && currentWeapon != null)
            currentWeapon.GetComponent<ShootingSystem>().Shoot();
    }
}
