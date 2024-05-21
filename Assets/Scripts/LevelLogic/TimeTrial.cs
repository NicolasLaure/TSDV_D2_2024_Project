using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrial : MonoBehaviour
{
    [SerializeField] private float trialDuration;

    private Coroutine trial;
    private void Start()
    {
        //suscribe to start level
        OnStartTrial();
    }

    private void OnStartTrial()
    {
        if (trial != null)
            StopCoroutine(trial);
        trial = StartCoroutine(TrialCoroutine());
    }
    private IEnumerator TrialCoroutine()
    {
        float startTime = Time.time;
        float timer = 0;
        while (timer < trialDuration)
        {
            timer = Time.time - startTime;
            Debug.Log($"Time left {trialDuration - timer}");
            yield return null;
        }
            Debug.Log($"Trial Finish");
    }
}
