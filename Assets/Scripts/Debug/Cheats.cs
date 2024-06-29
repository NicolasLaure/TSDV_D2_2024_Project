using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    [SerializeField] private List<WeaponSO> weaponSOs = new List<WeaponSO>();
    [SerializeField] private MagazineSO cheatMag;
    private CheatsInput cheats;

    private void Awake()
    {
        cheats = new CheatsInput();
        cheats.Enable();

        cheats.Cheats.MaxAmmo.performed += InfiniteAmmo;
        cheats.Cheats.NextLevel.performed += NextScene;
        cheats.Cheats.PrevLevel.performed += PrevScene;
    }
    private void InfiniteAmmo(InputAction.CallbackContext context)
    {
        foreach (WeaponSO weapon in weaponSOs)
        {
            if (weapon.CurrentMagazine != cheatMag)
                weapon.CurrentMagazine = cheatMag;
            else
                weapon.SetDefault();
        }
    }
    private void NextScene(InputAction.CallbackContext context)
    {
        Loader.ChangeToNextScene();
    }
    private void PrevScene(InputAction.CallbackContext context)
    {
        Loader.ChangeToPreviousScene();
    }
}