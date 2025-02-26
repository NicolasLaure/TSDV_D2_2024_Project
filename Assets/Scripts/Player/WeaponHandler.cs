using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform holdingPoint;
    [SerializeField] private WeaponSO currentWeaponSO = null;
    [SerializeField] private int maxHeldWeaponsQty;
    [SerializeField] private float dropForce;
    [SerializeField] private List<GameObject> weaponPrefabs = new List<GameObject>();
    private List<GameObject> weapons = new List<GameObject>();
    private List<GameObject> heldWeapons = new List<GameObject>();
    private GameObject currentWeaponObject = null;
    private int currentWeaponIndex = 0;
    private bool isInCombatMode = true;
    private WeaponRumble weaponRumbleController;
    private CharacterMovement playerMovement;

    public Action<Weapon> onWeaponChange;
    public WeaponSO CurrentWeaponSO { get { return currentWeaponSO; } }
    public Weapon CurrentWeapon
    {
        get
        {
            if (currentWeaponObject)
                return currentWeaponObject.GetComponent<Weapon>();
            else
                return null;
        }
    }

    private void Awake()
    {
        playerMovement = GetComponent<CharacterMovement>();

        foreach (GameObject weapon in weaponPrefabs)
        {
            weapon.GetComponent<Weapon>().WeaponSO.SetDefault();
        }
    }
    private void Start()
    {
        onWeaponChange += OnWeaponChanged;
        foreach (GameObject weapon in weaponPrefabs)
        {
            GameObject instance = Instantiate(weapon, holdingPoint);
            instance.GetComponent<Weapon>().pivot = Camera.main.transform;
            weapons.Add(instance);
            instance.SetActive(false);
        }
        currentWeaponIndex = 0;
        heldWeapons.Add(weapons[0]);
        onWeaponChange?.Invoke(heldWeapons[currentWeaponIndex].GetComponent<Weapon>());

        playerMovement.onCharacterMove += SetWalkingState;
        playerMovement.onCharacterSprint += SetSprint;
    }
    private void OnDestroy()
    {
        onWeaponChange -= OnWeaponChanged;
        playerMovement.onCharacterMove -= SetWalkingState;
        playerMovement.onCharacterSprint -= SetSprint;
    }

    public void OnWeaponChanged<T>(T weapon) where T : Weapon
    {
        if (currentWeaponObject != null)
        {
            SetWalkingState(Vector2.zero);
            SetSprint(false);
            currentWeaponObject.SetActive(false);
        }

        currentWeaponObject = weapon.gameObject;
        currentWeaponObject.SetActive(true);
        weaponRumbleController = currentWeaponObject.GetComponent<WeaponRumble>();
    }

    public void ShootWeapon()
    {
        if (isInCombatMode && currentWeaponObject != null)
            currentWeaponObject.GetComponent<Weapon>().Shoot();
    }
    public void CancelShoot()
    {
        if (currentWeaponObject != null)
            currentWeaponObject.GetComponent<Weapon>().StopShooting();
    }
    public void SetWeaponRumbler(bool value)
    {
        weaponRumbleController.ShouldRumble = value;
    }

    public void ReloadWeapon()
    {
        if (isInCombatMode && currentWeaponObject != null)
            currentWeaponObject.GetComponent<Weapon>().Reload();
    }

    public void SetWalkingState(Vector2 movementDir)
    {
        currentWeaponObject.GetComponent<Weapon>().OnMovementChange(movementDir);
    }
    public void SetSprint(bool shouldPlayRunAnim)
    {
        currentWeaponObject.GetComponent<Weapon>().OnSprintChange(shouldPlayRunAnim);
    }

    public void ScrollThroughHeldWeapons(int value)
    {
        currentWeaponIndex += value;
        if (currentWeaponIndex < 0)
            currentWeaponIndex = heldWeapons.Count - 1;
        else if (currentWeaponIndex >= heldWeapons.Count)
            currentWeaponIndex = 0;

        onWeaponChange?.Invoke(heldWeapons[currentWeaponIndex].GetComponent<Weapon>());
    }

    public bool TryGrabWeapon(WeaponSO weapon)
    {
        GameObject weaponsInstance = null;
        foreach (GameObject weaponGameObject in weapons)
        {
            if (weaponGameObject.GetComponent<Weapon>().WeaponSO == weapon)
                weaponsInstance = weaponGameObject;
        }

        if (heldWeapons.Contains(weaponsInstance))
            return false;

        if (heldWeapons.Count < maxHeldWeaponsQty)
        {
            heldWeapons.Add(weaponsInstance);
            return true;
        }

        heldWeapons.RemoveAt(currentWeaponIndex);
        heldWeapons.Insert(currentWeaponIndex, weaponsInstance);
        onWeaponChange?.Invoke(heldWeapons[currentWeaponIndex].GetComponent<Weapon>());
        return true;
    }

    public bool TryDropWeapon()
    {
        if (heldWeapons.Count > 1)
        {
            GameObject weaponPrefab = heldWeapons[currentWeaponIndex].GetComponent<Weapon>().EnvironmentWeaponPrefab;
            GameObject weapon = GameObject.Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            StartCoroutine(weapon.GetComponent<EnvironmentWeapon>().ThrowWeapon(Camera.main.transform.forward * dropForce));
            heldWeapons.RemoveAt(currentWeaponIndex);
            onWeaponChange?.Invoke(heldWeapons[0].GetComponent<Weapon>());
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnvironmentWeapon>(out EnvironmentWeapon weapon))
        {
            if (TryGrabWeapon(weapon.Weapon))
                Destroy(other.gameObject);
        }
    }
}
