using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    private WeaponHandler playerWeaponHandler;

    private string currentAmmo;
    private string magazineSize;
    private Weapon currentWeapon = null;


    [SerializeField] private TMPro.TextMeshProUGUI ammoText;
    [SerializeField] private Slider ammoSlider;

    private void Awake()
    {
        playerWeaponHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponHandler>();
        playerWeaponHandler.onWeaponChange += OnWeaponChange;
    }
    void OnWeaponChange<T>(T weapon) where T : Weapon
    {
        if (currentWeapon != null)
        {
            currentWeapon.onShoot.RemoveListener(OnAmmoModified);
            currentWeapon.onReloadFinished.RemoveListener(OnAmmoModified);
        }

        currentWeapon = weapon;
        WeaponSO weaponData = weapon.WeaponSO;
        currentAmmo = weapon.CurrentAmmo.ToString();
        magazineSize = "/" + weaponData.MagazineSize;
        ammoSlider.maxValue = weaponData.MagazineSize;
        SetUiElements();

        currentWeapon.onShoot.AddListener(OnAmmoModified);
        currentWeapon.onReloadFinished.AddListener(OnAmmoModified);
    }

    void OnAmmoModified()
    {
        currentAmmo = currentWeapon.CurrentAmmo.ToString();
        SetUiElements();
    }

    private void SetUiElements()
    {
        magazineSize = "/" + currentWeapon.WeaponSO.MagazineSize;
        ammoText.text = currentAmmo + magazineSize;
        ammoSlider.value = currentWeapon.CurrentAmmo;
    }
}
