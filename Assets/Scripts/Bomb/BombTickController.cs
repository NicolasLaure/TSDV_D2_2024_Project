using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BombTickController : MonoBehaviour
{
    [SerializeField] private Light redLight;
    [SerializeField] private float pingPongDuration;

    [Header("Fuse White Light")]
    [SerializeField] private Light whiteLight;
    [SerializeField] private AnimationCurve intensityOverTime;

    private float initialIntensity;
    private float variationPerSecond;
    private bool isTurningOn;

    private void Start()
    {
        initialIntensity = redLight.intensity;
        variationPerSecond = initialIntensity / pingPongDuration;
    }

    private void Update()
    {
        UpdateLightIntensity();
    }

    private void UpdateLightIntensity()
    {
        float currentIntensity = Mathf.Clamp(redLight.intensity + variationPerSecond * Time.deltaTime, 0, initialIntensity);
        redLight.intensity = currentIntensity;

        if (currentIntensity == initialIntensity || currentIntensity == 0)
            variationPerSecond *= -1;
    }

    public void StartFuse(float duration)
    {
        redLight.enabled = false;
        whiteLight.enabled = true;
        StartCoroutine(WhiteLightCoroutine(duration));
    }

    private IEnumerator WhiteLightCoroutine(float duration)
    {
        float startTime = Time.time;
        float timer = 0;

        while (timer <= duration)
        {
            whiteLight.intensity = intensityOverTime.Evaluate(timer / duration);
            timer = Time.time - startTime;
            yield return null;
        }
    }
}