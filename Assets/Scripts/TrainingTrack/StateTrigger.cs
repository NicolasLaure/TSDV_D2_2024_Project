using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTrigger : MonoBehaviour
{
    [SerializeField] private TrainingTrackManager trackManager;
    [SerializeField] private string stateName;
    private bool wasPlayed = false;

    private void Awake()
    {
        trackManager.onRestart += ResetTrigger;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!wasPlayed)
        {
            trackManager.onStateChange?.Invoke(stateName);
            wasPlayed = true;
        }
    }
    public void ResetTrigger()
    {
        wasPlayed = false;
    }
}
