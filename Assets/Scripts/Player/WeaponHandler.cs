using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform holdingPoint;
    [SerializeField] private WeaponSO currentWeaponSO = null;
    [SerializeField] private List<GameObject> weaponPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    private GameObject currentWeapon = null;
    private bool isInCombatMode = true;

    public Action<Weapon> onWeaponChange;
    public WeaponSO CurrentWeaponSO { get { return currentWeaponSO; } }

    private void Awake()
    {
        onWeaponChange += OnWeaponGrab;
    }
    private void Start()
    {
        foreach (GameObject weapon in weaponPrefabs)
        {
            GameObject instance = Instantiate(weapon, holdingPoint);
            weapons.Add(instance);
            instance.SetActive(false);
        }
        onWeaponChange.Invoke(weapons[0].GetComponent<Weapon>());
    }

    public void OnWeaponGrab<T>(T weapon) where T : Weapon
    {
        if (currentWeapon != null)
            currentWeapon.SetActive(false);

        currentWeapon = weapon.gameObject;
        currentWeapon.SetActive(true);
    }

    public void ShootWeapon()
    {
        if (isInCombatMode && currentWeapon != null)
            currentWeapon.GetComponent<Weapon>().Shoot();
    }

    public void CancelShoot()
    {
        if (isInCombatMode && currentWeapon != null)
            currentWeapon.GetComponent<Weapon>().StopShooting();
    }

    public void ReloadWeapon()
    {
        if (isInCombatMode && currentWeapon != null)
            currentWeapon.GetComponent<Weapon>().Reload();
    }
}
