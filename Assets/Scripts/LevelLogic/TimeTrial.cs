using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeTrial : MonoBehaviour
{
    [SerializeField] private float trialDuration;
    [SerializeField] private Target target;
    [SerializeField] private List<Transform> possiblePositions = new List<Transform>();
    private Coroutine trial;

    public Action onTrialFinish;
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
            yield return null;
        }
        onTrialFinish.Invoke();
    }

    private void PresentTarget()
    {
        Target target = targets[UnityEngine.Random.Range(0, targets.Count)];

        target.transform.position = GetRandomPosition();
        target.gameObject.SetActive(true);
    }

    private Vector3 GetRandomPosition()
    {
        int randomIndex = UnityEngine.Random.Range(0, possiblePositions.Count);
        while (possiblePositions[randomIndex].heldObject == null)
            randomIndex = UnityEngine.Random.Range(0, possiblePositions.Count);

        return possiblePositions[randomIndex].transform.position;
    }
}
