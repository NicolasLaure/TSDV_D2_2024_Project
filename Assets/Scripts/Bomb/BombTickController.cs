using System.Collections;
using UnityEngine;

public class BombTickController : MonoBehaviour
{
    [SerializeField] private Light redLight;
    [SerializeField] private float pingPongDuration;

    [Header("Fuse White Light")]
    [SerializeField] private Light whiteLight;
    [SerializeField] private AnimationCurve intensityOverTime;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Sound beepingSound;
    [SerializeField] private Sound initializeSound;

    private float maxIntensity;
    private float variationPerSecond;
    private bool isTurningOn;

    private void Start()
    {
        maxIntensity = redLight.intensity;
        variationPerSecond = maxIntensity / pingPongDuration;
        redLight.intensity = Random.Range(0, maxIntensity);
    }

    private void Update()
    {
        UpdateLightIntensity();
    }

    private void UpdateLightIntensity()
    {
        float currentIntensity = Mathf.Clamp(redLight.intensity + variationPerSecond * Time.deltaTime, 0, maxIntensity);
        redLight.intensity = currentIntensity;

        if (currentIntensity == maxIntensity || currentIntensity == 0)
        {
            variationPerSecond *= -1;
            if (currentIntensity == maxIntensity)
                soundManager.PlayOnce(beepingSound.name);
        }
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
        soundManager.PlayOnce(initializeSound.name);

        while (timer <= duration)
        {
            whiteLight.intensity = intensityOverTime.Evaluate(timer / duration);
            timer = Time.time - startTime;
            yield return null;
        }
    }
}