using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTickController : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private float pingPongDuration;

    private float initialIntensity;
    private float variationPerSecond;
    private bool isTurningOn;

    private void Start()
    {
        initialIntensity = light.intensity;
        variationPerSecond = initialIntensity / pingPongDuration;
    }

    private void Update()
    {
        UpdateLightIntensity();
    }

    private void UpdateLightIntensity()
    {
        float currentIntensity = Mathf.Clamp(light.intensity + variationPerSecond * Time.deltaTime, 0, initialIntensity);
        light.intensity = currentIntensity;

        if (currentIntensity == initialIntensity || currentIntensity == 0)
            variationPerSecond *= -1;
    }
}