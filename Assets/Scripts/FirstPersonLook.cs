using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Quaternion rotation;
    private void Update()
    {
        
    }

    public void RotateTowards(Vector2 dir)
    {
        transform.Rotate(Vector3.up, dir.x * rotationSpeed * Time.deltaTime);
        Camera.main.transform.Rotate(Vector3.right, -dir.y * rotationSpeed * Time.deltaTime);
    }
}
