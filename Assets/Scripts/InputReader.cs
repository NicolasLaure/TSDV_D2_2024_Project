using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
public class InputReader : MonoBehaviour
{
    private PlayerInput input;
    private CharacterMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<CharacterMovement>();
        input = new PlayerInput();
        input.Enable();

        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        playerMovement.SetDir(context.ReadValue<Vector2>());
    }
    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        playerMovement.SetDir(Vector2.zero);
    }
}
