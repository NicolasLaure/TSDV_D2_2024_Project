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
    private float XAxisAngle = 0;
    private float YAxisAngle = 0;
    Vector2 rotationDir = Vector2.zero;

    [Header("RecoilEvents")]
    [Tooltip("X represents Horizontal Displacement (Y Axis rotation), Y represents Vertical Displacement (X Axis rotation), Z represents progress from (0,1) ")]
    [SerializeField] private Vector3EventChannelSO recoilAngleEvent;
    [SerializeField] private FloatChannelEvent resetRecoilEvent;
    private Vector2 originAxisAngles;
    private bool isRecoiling;


    private Vector2 crossHairDisplace;

    private void Start()
    {
        mouseSensitivity = playerConfig.lookSensitivity;
        SetMouseLockState(true);
        XAxisAngle = Camera.main.transform.localRotation.eulerAngles.x;
        YAxisAngle = transform.rotation.eulerAngles.y;
    }

    private void OnEnable()
    {
        recoilAngleEvent.onVector3Event += Recoil;
        resetRecoilEvent.onFloatEvent += ResetRecoil;
    }

    private void OnDisable()
    {
        recoilAngleEvent.onVector3Event -= Recoil;
        resetRecoilEvent.onFloatEvent -= ResetRecoil;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationDir.x * mouseSensitivity);
        XAxisAngle += rotationDir.y * mouseSensitivity;
        XAxisAngle = Mathf.Clamp(XAxisAngle, -maxVerticalRotation, maxVerticalRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(-XAxisAngle, 0, 0);
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
            originAxisAngles.x = XAxisAngle;
            originAxisAngles.y = YAxisAngle;
            Debug.Log($"origin Y axis angle: {originAxisAngles.y}");
        }

        originAxisAngles.x -= crossHairDisplace.y;
        originAxisAngles.y += crossHairDisplace.x;
        XAxisAngle = originAxisAngles.x - newAngle.x;
        YAxisAngle = originAxisAngles.y + newAngle.y;

        transform.rotation = Quaternion.Euler(0, YAxisAngle, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(-XAxisAngle, 0, 0);
    }

    private void ResetRecoil(float progress)
    {
        Quaternion targetCameraRotation = Quaternion.Euler(originAxisAngles.x, 0, 0);
        Quaternion targetPlayerRotation = Quaternion.Euler(0, originAxisAngles.y, 0);
        Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, targetCameraRotation, progress);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetPlayerRotation, progress);

        if (progress >= 1)
            isRecoiling = false;
    }
}