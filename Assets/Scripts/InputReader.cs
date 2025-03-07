using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(FirstPersonLook))]
public class InputReader : MonoBehaviour
{
    private PlayerInput _input;
    private CharacterMovement _playerMovement;
    private FirstPersonLook _playerLook;
    private WeaponHandler _weaponHandler;
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        _playerMovement = GetComponent<CharacterMovement>();
        _playerLook = GetComponent<FirstPersonLook>();
        _weaponHandler = GetComponent<WeaponHandler>();

        StartCoroutine(InitializeInput());
    }

    private void OnDisable()
    {
        if (_input != null)
        {
            RemoveListeners();
            _input.Dispose();
        }
    }

    private IEnumerator InitializeInput()
    {
        yield return null;
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
        _input.Player.Sprint.performed += OnSprintPerformed;
        _input.Player.Sprint.canceled += OnSprintCanceled;
        _input.Player.Look.performed += OnLookPerformed;
        _input.Player.Look.canceled += OnLookCanceled;
        _input.Player.Shoot.performed += OnShootPerformed;
        _input.Player.Shoot.canceled += OnShootCanceled;
        _input.Player.Reload.started += OnReloadStarted;
        _input.Player.ChangeWeapon.started += OnWeaponChange;
        _input.Player.Drop.performed += OnWeaponDrop;

        if (pausePanel)
        {
            pausePanel.GetComponent<Pause>().onPausePanelStateChange += OnPausePanelChange;
            _input.Game.Pause.performed += OnPause;
        }
    }

    private void RemoveListeners()
    {
        _input.Player.Movement.performed -= OnMovementPerformed;
        _input.Player.Movement.canceled -= OnMovementCanceled;
        _input.Player.Look.performed -= OnLookPerformed;
        _input.Player.Look.canceled -= OnLookCanceled;
        _input.Player.Shoot.performed -= OnShootPerformed;
        _input.Player.Shoot.canceled -= OnShootCanceled;
        _input.Player.Reload.started -= OnReloadStarted;
        _input.Player.ChangeWeapon.started -= OnWeaponChange;
        _input.Player.Drop.performed -= OnWeaponDrop;
        _input.Game.Pause.performed -= OnPause;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        _playerMovement.SetDir(dir);
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        _playerMovement.SetDir(Vector2.zero);
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        _playerMovement.SetSprint(true);
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        _playerMovement.SetSprint(false);
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _playerLook.SetRotation(context.ReadValue<Vector2>());
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        _playerLook.SetRotation(context.ReadValue<Vector2>());
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        _weaponHandler.ShootWeapon();
        _weaponHandler.SetWeaponRumbler(context.control.device is Gamepad);
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        _weaponHandler.CancelShoot();
    }

    private void OnReloadStarted(InputAction.CallbackContext context)
    {
        _weaponHandler.ReloadWeapon();
    }

    private void OnWeaponChange(InputAction.CallbackContext context)
    {
        int axisValue = (int)Mathf.Ceil(context.ReadValue<float>());
        _weaponHandler.ScrollThroughHeldWeapons(axisValue);
    }

    private void OnWeaponDrop(InputAction.CallbackContext context)
    {
        _weaponHandler.TryDropWeapon();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }

    private void OnPausePanelChange(bool value)
    {
        if (value == true)
            _input.Player.Disable();
        else
            _input.Player.Enable();
    }
}