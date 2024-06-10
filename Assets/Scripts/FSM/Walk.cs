using System;
using UnityEngine;
public class Walk : State
{
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeedMultiplier;
    private float currentMultiplier = 1;
    private CharacterController characterController;
    private Vector3 localMovementDir;

    public Action<Vector2> onCharacterMove;
    public Action<bool> onCharacterSprint;

    public override void Enter(GameObject characterGameObject)
    {
        base.Enter(characterGameObject);
        characterController = characterGameObject.GetComponent<CharacterController>();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
    public override void Update()
    {
        Move();
    }

    public override void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void LateUpdate()
    {
        throw new System.NotImplementedException();
    }


    private void Move()
    {
        if (localMovementDir == Vector3.zero)
        {
            SetSprint(false);
            return;
        }

        Vector3 movementDir = characterGameObject.transform.right * localMovementDir.x + characterGameObject.transform.forward * localMovementDir.z;
        characterController.Move(movementDir * speed * currentMultiplier * Time.deltaTime);
    }
    public void SetDir(Vector2 dir)
    {
        localMovementDir = new Vector3(dir.x, 0, dir.y);
        onCharacterMove.Invoke(dir);
    }
    public void SetSprint(bool isSprinting)
    {
        if (isSprinting && localMovementDir != Vector3.zero)
        {
            currentMultiplier = sprintSpeedMultiplier;
            onCharacterSprint.Invoke(true);
        }
        else
        {
            currentMultiplier = 1;
            onCharacterSprint.Invoke(false);
        }
    }
}
