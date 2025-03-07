using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private WeaponHandler playerWeaponHandler;

    private string _currentAmmo;
    private string _magazineSize;
    private Weapon _currentWeapon = null;

    [SerializeField] private TextMeshProUGUI ammoText;

    private void Start()
    {
        if (playerWeaponHandler.CurrentWeapon != null)
            OnWeaponChange(playerWeaponHandler.CurrentWeapon);

        playerWeaponHandler.onWeaponChange += OnWeaponChange;
    }

    private void OnDestroy()
    {
        playerWeaponHandler.onWeaponChange -= OnWeaponChange;
    }

    void OnWeaponChange<T>(T weapon) where T : Weapon
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.onShoot.RemoveListener(OnAmmoModified);
            _currentWeapon.onReloadFinished.RemoveListener(OnAmmoModified);
        }

        _currentWeapon = weapon;
        WeaponSO weaponData = weapon.WeaponSO;
        _currentAmmo = weapon.CurrentAmmo.ToString();
        _magazineSize = "/" + weaponData.MagazineSize;
        SetUiElements();

        _currentWeapon.onShoot.AddListener(OnAmmoModified);
        _currentWeapon.onReloadFinished.AddListener(OnAmmoModified);
    }

    void OnAmmoModified()
    {
        _currentAmmo = _currentWeapon.CurrentAmmo.ToString();
        SetUiElements();
    }

    private void SetUiElements()
    {
        _magazineSize = "/" + _currentWeapon.WeaponSO.MagazineSize;
        ammoText.text = _currentAmmo + _magazineSize;
    }
}