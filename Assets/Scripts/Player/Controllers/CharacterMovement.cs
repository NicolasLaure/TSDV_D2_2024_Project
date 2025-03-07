using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private PlayerSpeedSO speedSO;
    [SerializeField] private float sprintSpeedMultiplier;
    private float _currentMultiplier = 1;
    private CharacterController _characterController;
    private Vector3 _localMovementDir;

    public Action<Vector2> onCharacterMove;
    public Action<bool> onCharacterSprint;

    public PlayerSpeedSO SpeedSo
    {
        get => speedSO;
        set => speedSO = value;
    }
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_localMovementDir == Vector3.zero)
        {
            SetSprint(false);
            return;
        }

        Vector3 movementDir = transform.right * _localMovementDir.x + transform.forward * _localMovementDir.z;
        Vector3 gravity = Physics.gravity;
        _characterController.Move(gravity + movementDir * speedSO.speed * _currentMultiplier * Time.deltaTime);
    }

    public void SetDir(Vector2 dir)
    {
        _localMovementDir = new Vector3(dir.x, 0, dir.y);
        onCharacterMove?.Invoke(dir);
    }

    public void SetSprint(bool isSprinting)
    {
        if (isSprinting && _localMovementDir != Vector3.zero)
        {
            _currentMultiplier = sprintSpeedMultiplier;
            onCharacterSprint?.Invoke(true);
        }
        else
        {
            _currentMultiplier = 1;
            onCharacterSprint?.Invoke(false);
        }
    }
}