using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeedMultiplier;
    private float currentMultiplier = 1;
    private CharacterController characterController;
    private Vector3 localMovementDir;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (localMovementDir == Vector3.zero)
            return;

        Vector3 movementDir = transform.right * localMovementDir.x + transform.forward * localMovementDir.z;
        characterController.Move(movementDir * speed * currentMultiplier * Time.deltaTime);
    }
    public void SetDir(Vector2 dir)
    {
        localMovementDir = new Vector3(dir.x, 0, dir.y);
    }
    public void SetSprint(bool isSprinting)
    {
        if (isSprinting && localMovementDir != Vector3.zero)
            currentMultiplier = sprintSpeedMultiplier;
        else
            currentMultiplier = 1;
    }
}
