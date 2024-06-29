using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponEffectsHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent methodsToInvoke;
    public void InvokeEvent(float time)
    {
        StartCoroutine(InvokeAfterCoroutine(time));
    }

    IEnumerator InvokeAfterCoroutine(float duration)
    {
        float startTime = Time.time;
        float timer = 0;
        while (timer< duration)
        {
            timer = Time.time - startTime;
            yield return null;
        }
        methodsToInvoke?.Invoke();
    }
}
