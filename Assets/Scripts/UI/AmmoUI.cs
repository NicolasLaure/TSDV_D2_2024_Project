using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    private WeaponHandler playerWeaponHandler;

    private string currentAmmo;
    private string magazineSize;
    void Start()
    {
    }

    void OnWeaponChange()
    {
        //WeaponSO Should have magazineSize and relevant data
        magazineSize = "/";
    }

    void OnAmmoModified()
    {

    }
}
