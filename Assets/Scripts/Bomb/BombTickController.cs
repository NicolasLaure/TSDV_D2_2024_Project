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

    private float _maxIntensity;
    private float _variationPerSecond;
    private bool _isTurningOn;

    private void Start()
    {
        _maxIntensity = redLight.intensity;
        _variationPerSecond = _maxIntensity / pingPongDuration;
        redLight.intensity = Random.Range(0, _maxIntensity);
    }

    private void Update()
    {
        UpdateLightIntensity();
    }

    private void UpdateLightIntensity()
    {
        float currentIntensity = Mathf.Clamp(redLight.intensity + _variationPerSecond * Time.deltaTime, 0, _maxIntensity);
        redLight.intensity = currentIntensity;

        if (currentIntensity == _maxIntensity || currentIntensity == 0)
        {
            _variationPerSecond *= -1;
            if (currentIntensity == _maxIntensity)
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