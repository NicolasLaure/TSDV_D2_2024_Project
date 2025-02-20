using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] PlayerConfigSO playerConfig;
    private float mouseSensitivity;
    [SerializeField] private float maxVerticalRotation;
    private float rotX = 0;
    Vector2 rotationDir = Vector2.zero;

    [Header("RecoilEvents")]
    [Tooltip("X represents Horizontal Displacement (Y Axis rotation), Y represents Vertical Displacement (X Axis rotation), Z represents progress from (0,1) ")]
    [SerializeField] private Vector3EventChannelSO recoilAngleEvent;
    [SerializeField] private VoidEventChannelSO resetRecoilEvent;
    private Vector2 originAxisAngles;
    private bool isRecoiling;


    private Vector2 crossHairDisplace;

    private void Start()
    {
        mouseSensitivity = playerConfig.lookSensitivity;
        SetMouseLockState(true);
    }

    private void OnEnable()
    {
        recoilAngleEvent.onVector3Event += Recoil;
        resetRecoilEvent.onVoidEvent += ResetRecoil;
    }

    private void OnDisable()
    {
        recoilAngleEvent.onVector3Event -= Recoil;
        resetRecoilEvent.onVoidEvent -= ResetRecoil;
    }

    private void Update()
    {
        if (rotationDir == Vector2.zero)
            return;

        transform.Rotate(Vector3.up, rotationDir.x * mouseSensitivity);
        rotX += rotationDir.y * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -maxVerticalRotation, maxVerticalRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
    }

    public void RotateTowards(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            crossHairDisplace = Vector2.zero;
            return;
        }

        crossHairDisplace = new Vector2(dir.x * mouseSensitivity, dir.y * mouseSensitivity);

        if (!isRecoiling)
        {
            transform.Rotate(Vector3.up, crossHairDisplace.x);
            rotX += crossHairDisplace.y;
            rotX = Mathf.Clamp(rotX, -maxVerticalRotation, maxVerticalRotation);
            Camera.main.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        }
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

    private void Recoil(Vector3 newAngle)
    {
        isRecoiling = true;
        if (newAngle.z == 0)
        {
            originAxisAngles.x = newAngle.x;
            originAxisAngles.y = newAngle.y;
        }

        originAxisAngles.x -= crossHairDisplace.y;
        originAxisAngles.y += crossHairDisplace.x;
        transform.rotation = Quaternion.Euler(0, originAxisAngles.y + newAngle.y, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(originAxisAngles.x + newAngle.x, 0, 0);
    }

    private void ResetRecoil()
    {
        rotX = originAxisAngles.x;
        Camera.main.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        isRecoiling = false;
    }
}