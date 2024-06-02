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
    [SerializeField] private GameObject pausePanel;
    void Start()
    {
        playerMovement = GetComponent<CharacterMovement>();
        playerLook = GetComponent<FirstPersonLook>();
        weaponHandler = GetComponent<WeaponHandler>();

        StartCoroutine(InitializeInput());
    }
    private void OnDestroy()
    {
        if (input != null)
        {
            RemoveListeners();
            input.Dispose();
        }
    }
    private IEnumerator InitializeInput()
    {
        yield return null;
        input = new PlayerInput();
        input.Enable();

        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
        input.Player.Sprint.performed += OnSprintPerformed;
        input.Player.Sprint.canceled += OnSprintCanceled;
        input.Player.Look.performed += OnLookPerformed;
        input.Player.Look.canceled += OnLookCanceled;
        input.Player.Shoot.performed += OnShootPerformed;
        input.Player.Shoot.canceled += OnShootCanceled;
        input.Player.Reload.started += OnReloadStarted;
        input.Player.ChangeWeapon.started += OnWeaponChange;
        input.Player.Drop.performed += OnWeaponDrop;

        pausePanel.GetComponent<Pause>().onPausePanelStateChange += OnPausePanelChange;
        input.Game.Pause.performed += OnPause;
    }

    private void RemoveListeners()
    {
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
        input.Player.Look.performed -= OnLookPerformed;
        input.Player.Look.canceled -= OnLookCanceled;
        input.Player.Shoot.performed -= OnShootPerformed;
        input.Player.Shoot.canceled -= OnShootCanceled;
        input.Player.Reload.started -= OnReloadStarted;
        input.Player.ChangeWeapon.started -= OnWeaponChange;
        input.Player.Drop.performed -= OnWeaponDrop;
        input.Game.Pause.performed -= OnPause;

    }
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        playerMovement.SetDir(dir);
        weaponHandler.SetWalkingState(dir);
    }
    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        playerMovement.SetDir(Vector2.zero);
        weaponHandler.SetWalkingState(Vector2.zero);
    }
    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        playerMovement.SetSprint(true);
    }
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        playerMovement.SetSprint(false);
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
        weaponHandler.ScrollThroughHeldWeapons(axisValue);
    }
    private void OnWeaponDrop(InputAction.CallbackContext context)
    {
        weaponHandler.TryDropWeapon();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }

    private void OnPausePanelChange(bool value)
    {
        if (value == true)
            input.Player.Disable();
        else
            input.Player.Enable();
    }
}
