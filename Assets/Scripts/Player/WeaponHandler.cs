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
    [SerializeField] private bool hasAllWeapons;
    private List<GameObject> _weapons = new List<GameObject>();
    private List<GameObject> _heldWeapons = new List<GameObject>();
    private GameObject _currentWeaponObject = null;
    private int _currentWeaponIndex = 0;
    private bool _isInCombatMode = true;
    private WeaponRumble _weaponRumbleController;
    private CharacterMovement _playerMovement;

    public Action<Weapon> onWeaponChange;

    public WeaponSO CurrentWeaponSO
    {
        get { return currentWeaponSO; }
    }

    public Weapon CurrentWeapon
    {
        get
        {
            if (_currentWeaponObject)
                return _currentWeaponObject.GetComponent<Weapon>();
            else
                return null;
        }
    }

    private void Awake()
    {
        _playerMovement = GetComponent<CharacterMovement>();

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
            _weapons.Add(instance);
            instance.SetActive(false);
        }

        _currentWeaponIndex = 0;

        if (hasAllWeapons)
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                _heldWeapons.Add(_weapons[i]);
            }
        }
        else
            _heldWeapons.Add(_weapons[0]);

        onWeaponChange?.Invoke(_heldWeapons[_currentWeaponIndex].GetComponent<Weapon>());

        _playerMovement.onCharacterMove += SetWalkingState;
        _playerMovement.onCharacterSprint += SetSprint;
    }

    private void OnDestroy()
    {
        onWeaponChange -= OnWeaponChanged;
        _playerMovement.onCharacterMove -= SetWalkingState;
        _playerMovement.onCharacterSprint -= SetSprint;
    }

    public void OnWeaponChanged<T>(T weapon) where T : Weapon
    {
        if (_currentWeaponObject != null)
        {
            SetWalkingState(Vector2.zero);
            SetSprint(false);
            _currentWeaponObject.SetActive(false);
        }

        _currentWeaponObject = weapon.gameObject;
        _currentWeaponObject.SetActive(true);
        _weaponRumbleController = _currentWeaponObject.GetComponent<WeaponRumble>();
    }

    public void ShootWeapon()
    {
        if (_isInCombatMode && _currentWeaponObject != null)
            _currentWeaponObject.GetComponent<Weapon>().Shoot();
    }

    public void CancelShoot()
    {
        if (_currentWeaponObject != null)
            _currentWeaponObject.GetComponent<Weapon>().StopShooting();
    }

    public void SetWeaponRumbler(bool value)
    {
        _weaponRumbleController.ShouldRumble = value;
    }

    public void ReloadWeapon()
    {
        if (_isInCombatMode && _currentWeaponObject != null)
            _currentWeaponObject.GetComponent<Weapon>().Reload();
    }

    public void SetWalkingState(Vector2 movementDir)
    {
        _currentWeaponObject.GetComponent<Weapon>().OnMovementChange(movementDir);
    }

    public void SetSprint(bool shouldPlayRunAnim)
    {
        _currentWeaponObject.GetComponent<Weapon>().OnSprintChange(shouldPlayRunAnim);
    }

    public void ScrollThroughHeldWeapons(int value)
    {
        _currentWeaponIndex += value;
        if (_currentWeaponIndex < 0)
            _currentWeaponIndex = _heldWeapons.Count - 1;
        else if (_currentWeaponIndex >= _heldWeapons.Count)
            _currentWeaponIndex = 0;

        onWeaponChange?.Invoke(_heldWeapons[_currentWeaponIndex].GetComponent<Weapon>());
    }

    public bool TryGrabWeapon(WeaponSO weapon)
    {
        GameObject weaponsInstance = null;
        foreach (GameObject weaponGameObject in _weapons)
        {
            if (weaponGameObject.GetComponent<Weapon>().WeaponSO == weapon)
                weaponsInstance = weaponGameObject;
        }

        if (_heldWeapons.Contains(weaponsInstance))
            return false;

        if (_heldWeapons.Count < maxHeldWeaponsQty)
        {
            _heldWeapons.Add(weaponsInstance);
            return true;
        }

        _heldWeapons.RemoveAt(_currentWeaponIndex);
        _heldWeapons.Insert(_currentWeaponIndex, weaponsInstance);
        onWeaponChange?.Invoke(_heldWeapons[_currentWeaponIndex].GetComponent<Weapon>());
        return true;
    }

    public bool TryDropWeapon()
    {
        if (_heldWeapons.Count > 1)
        {
            GameObject weaponPrefab = _heldWeapons[_currentWeaponIndex].GetComponent<Weapon>().EnvironmentWeaponPrefab;
            GameObject weapon = GameObject.Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            StartCoroutine(weapon.GetComponent<EnvironmentWeapon>().ThrowWeapon(Camera.main.transform.forward * dropForce));
            _heldWeapons.RemoveAt(_currentWeaponIndex);
            onWeaponChange?.Invoke(_heldWeapons[0].GetComponent<Weapon>());
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