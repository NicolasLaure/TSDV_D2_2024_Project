using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] PlayerConfigSO playerConfig;
    [SerializeField] private float maxVerticalRotation;
    private float _mouseSensitivity;
    private float _XAxisAngle = 0;
    private float _YAxisAngle = 0;
    private Vector2 _rotationDir = Vector2.zero;

    [Header("RecoilEvents")]
    [Tooltip("X represents Horizontal Displacement (Y Axis rotation), Y represents Vertical Displacement (X Axis rotation), Z represents progress from (0,1) ")]
    [SerializeField] private Vector3EventChannelSO recoilAngleEvent;
    [SerializeField] private FloatChannelEvent resetRecoilEvent;
    private Vector2 _originAxisAngles;
    private bool _isRecoiling;

    private Vector2 _crossHairDisplace;

    private void Start()
    {
        _mouseSensitivity = playerConfig.lookSensitivity;
        SetMouseLockState(true);
        _XAxisAngle = Camera.main.transform.localRotation.eulerAngles.x;
        _YAxisAngle = transform.rotation.eulerAngles.y;
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
        transform.Rotate(Vector3.up, _rotationDir.x * _mouseSensitivity);
        _XAxisAngle += _rotationDir.y * _mouseSensitivity;
        _XAxisAngle = Mathf.Clamp(_XAxisAngle, -maxVerticalRotation, maxVerticalRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(-_XAxisAngle, 0, 0);
    }

    public void SetRotation(Vector2 dir)
    {
        _rotationDir = dir;
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
        _mouseSensitivity = playerConfig.lookSensitivity;
    }

    private void Recoil(Vector3 newAngle)
    {
        _isRecoiling = true;
        if (newAngle.z == 0)
        {
            _originAxisAngles.x = _XAxisAngle;
            _originAxisAngles.y = _YAxisAngle;
            Debug.Log($"origin Y axis angle: {_originAxisAngles.y}");
        }

        _originAxisAngles.x -= _crossHairDisplace.y;
        _originAxisAngles.y += _crossHairDisplace.x;
        _XAxisAngle = _originAxisAngles.x - newAngle.x;
        _YAxisAngle = _originAxisAngles.y + newAngle.y;

        transform.rotation = Quaternion.Euler(0, _YAxisAngle, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(-_XAxisAngle, 0, 0);
    }

    private void ResetRecoil(float progress)
    {
        Quaternion targetCameraRotation = Quaternion.Euler(_originAxisAngles.x, 0, 0);
        Quaternion targetPlayerRotation = Quaternion.Euler(0, _originAxisAngles.y, 0);
        Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, targetCameraRotation, progress);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetPlayerRotation, progress);

        if (progress >= 1)
            _isRecoiling = false;
    }
}