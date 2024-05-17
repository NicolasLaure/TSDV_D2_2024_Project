using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRumble : MonoBehaviour
{
    Weapon weapon;
    WeaponSO weaponSO;
    public bool ShouldRumble { get; set; }

    void Start()
    {
        weapon = GetComponent<Weapon>();
        weapon.onShoot.AddListener(OnWeaponShot);

        weaponSO = weapon.WeaponSO;
    }
    void OnWeaponShot()
    {
        if (ShouldRumble)
            StartCoroutine(GamePadRumble(weaponSO.RumbleLowIntensity, weaponSO.RumbleHighIntensity, weaponSO.RumbleDuration));
    }
    private IEnumerator GamePadRumble(float lowFrequenceIntensity, float highFrequenceIntensity, float duration)
    {
        Gamepad.current.SetMotorSpeeds(lowFrequenceIntensity, highFrequenceIntensity);
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
