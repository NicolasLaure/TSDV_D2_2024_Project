using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] PlayerConfigSO playerConfig;
    private float mouseSensitivity;
    [SerializeField] private float maxVerticalRotation;
    private float rotX = 0;
    Vector2 rotationDir = Vector2.zero;
    private void Start()
    {
        mouseSensitivity = playerConfig.lookSensitivity;
        SetMouseLockState(true);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationDir.x * mouseSensitivity);
        rotX += rotationDir.y * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -maxVerticalRotation, maxVerticalRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
    }
    public void RotateTowards(Vector2 dir)
    {
        transform.Rotate(Vector3.up, dir.x * mouseSensitivity);
        rotX += dir.y * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -maxVerticalRotation, maxVerticalRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
    }
    public void SetRotation(Vector2 dir)
    {
        rotationDir = dir;
    }
    public void SetMouseLockState(bool shouldBeLocked)
    {
        if (shouldBeLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    public void SetSensitivity(float newSensitivity)
    {
        playerConfig.lookSensitivity = newSensitivity;
        mouseSensitivity = playerConfig.lookSensitivity;
    }
}
