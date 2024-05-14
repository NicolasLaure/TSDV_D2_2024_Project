using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float maxVerticalRotation;
    private float rotX = 0;
    private void Start()
    {
        SetMouseLockState(true);
    }

    public void RotateTowards(Vector2 dir)
    {
        transform.Rotate(Vector3.up, dir.x * mouseSensitivity);
        rotX += dir.y * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -maxVerticalRotation, maxVerticalRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(-rotX,0,0);
    }
    public void SetMouseLockState(bool shouldBeLocked)
    {
        if (shouldBeLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
