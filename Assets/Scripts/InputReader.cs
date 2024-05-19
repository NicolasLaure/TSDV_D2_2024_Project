using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(FirstPersonLook))]
public class InputReader : MonoBehaviour
{
    private PlayerInput input;
    private CharacterMovement playerMovement;
    private FirstPersonLook playerLook;
    private WeaponHandler weaponHandler;

    void Start()
    {
        playerMovement = GetComponent<CharacterMovement>();
        playerLook = GetComponent<FirstPersonLook>();
        weaponHandler = GetComponent<WeaponHandler>();
        input = new PlayerInput();
        input.Enable();

        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
        input.Player.Look.performed += OnLookPerformed;
        input.Player.Look.canceled += OnLookCanceled;
        input.Player.Shoot.performed += OnShootPerformed;
        input.Player.Shoot.canceled += OnShootCanceled;
        input.Player.Reload.started += OnReloadStarted;
        input.Player.ChangeWeapon.started += OnWeaponChange;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        playerMovement.SetDir(context.ReadValue<Vector2>());
    }
    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        playerMovement.SetDir(Vector2.zero);
    }
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        if (context.control.device is Mouse)
            playerLook.RotateTowards(context.ReadValue<Vector2>());
        else
            playerLook.SetRotation(context.ReadValue<Vector2>());
    }
    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
            playerLook.SetRotation(context.ReadValue<Vector2>());
    }
    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        weaponHandler.ShootWeapon();
        weaponHandler.SetWeaponRumbler(context.control.device is Gamepad);
    }
    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        weaponHandler.CancelShoot();
    }
    private void OnReloadStarted(InputAction.CallbackContext context)
    {
        weaponHandler.ReloadWeapon();
    }
    private void OnWeaponChange(InputAction.CallbackContext context)
    {
        int axisValue = (int)Mathf.Ceil(context.ReadValue<float>());
        weaponHandler.ScrollWeapon(axisValue);
    }
}
