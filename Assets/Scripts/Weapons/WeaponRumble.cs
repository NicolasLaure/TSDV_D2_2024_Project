using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRumble : MonoBehaviour
{
    private Weapon _weapon;
    private WeaponSO _weaponSO;
    public bool ShouldRumble { get; set; }

    void Start()
    {
        _weapon = GetComponent<Weapon>();
        _weapon.onShoot.AddListener(OnWeaponShot);

        _weaponSO = _weapon.WeaponSO;
    }
    void OnWeaponShot()
    {
        if (ShouldRumble)
            StartCoroutine(GamePadRumble(_weaponSO.RumbleLowIntensity, _weaponSO.RumbleHighIntensity, _weaponSO.RumbleDuration));
    }
    private IEnumerator GamePadRumble(float lowFrequenceIntensity, float highFrequenceIntensity, float duration)
    {
        Gamepad.current.SetMotorSpeeds(lowFrequenceIntensity, highFrequenceIntensity);
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
