using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 movementDir;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movementDir == Vector3.zero)
            return;

        transform.Translate(movementDir * speed * Time.deltaTime);
    }
    public void SetDir(Vector2 dir)
    {
        //movementDir = transform.TransformDirection(new Vector3(dir.x, 0, dir.y));
        movementDir = new Vector3(dir.x, 0, dir.y);
        movementDir.Scale(transform.forward);
    }
}
