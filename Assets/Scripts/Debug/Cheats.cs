using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    [SerializeField] private List<WeaponSO> weaponSOs = new List<WeaponSO>();
    [SerializeField] private MagazineSO cheatMag;
    [Header("God Mode")]
    [SerializeField] private HealthPoints playerHealth;
    [SerializeField] private GameObject godText;

    [Header("Speed Cheat")]
    [SerializeField] private CharacterMovement playerMovement;
    [SerializeField] private PlayerSpeedSO defaultSpeed;
    [SerializeField] private PlayerSpeedSO flashSpeed;


    private CheatsInput _cheats;

    private void Awake()
    {
        _cheats = new CheatsInput();
        _cheats.Enable();

        _cheats.Cheats.MaxAmmo.performed += InfiniteAmmo;
        _cheats.Cheats.NextLevel.performed += NextScene;
        _cheats.Cheats.PrevLevel.performed += PrevScene;
        _cheats.Cheats.Nuke.performed += Nuke;
        _cheats.Cheats.GodMode.performed += ToggleGod;
        _cheats.Cheats.Flash.performed += ToggleFlash;
    }

    private void OnDisable()
    {
        _cheats.Cheats.MaxAmmo.performed -= InfiniteAmmo;
        _cheats.Cheats.NextLevel.performed -= NextScene;
        _cheats.Cheats.PrevLevel.performed -= PrevScene;
        _cheats.Cheats.Nuke.performed -= Nuke;
        _cheats.Cheats.GodMode.performed -= ToggleGod;
        _cheats.Cheats.Flash.performed -= ToggleFlash;
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

    private void Nuke(InputAction.CallbackContext context)
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<HealthPoints>().Kill();
        }
    }

    private void ToggleGod(InputAction.CallbackContext context)
    {
        playerHealth.ToggleInvincibility();
        godText.SetActive(!godText.activeInHierarchy);
    }

    private void ToggleFlash(InputAction.CallbackContext context)
    {
        if (playerMovement.SpeedSo == defaultSpeed)
            playerMovement.SpeedSo = flashSpeed;
        else
            playerMovement.SpeedSo = defaultSpeed;
    }
}