using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponEffectsHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();
    public void InvokeEvent(WeaponEffectConfigSO effectConfig)
    {
        StartCoroutine(InvokeCoroutine(effectConfig));
    }
    public void InvokeSecuentialEvent(EffectSequenceSO effectConfigs)
    {
        StartCoroutine(InvokeSecuentialCoroutine(effectConfigs));
    }
    IEnumerator InvokeCoroutine(WeaponEffectConfigSO effectConfig)
    {
        float startTime = Time.time;
        float timer = 0;
        while (timer < effectConfig.delay)
        {
            timer = Time.time - startTime;
            yield return null;
        }
        audioSource.clip = effectConfig.clip;
        audioSource.Play();
        if (effectConfig.triggerParticles)
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
    }

    IEnumerator InvokeSecuentialCoroutine(EffectSequenceSO effectConfigs)
    {
        foreach (WeaponEffectConfigSO config in effectConfigs.configs)
        {
            yield return InvokeCoroutine(config);
        }
    }
}
