using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTrigger : MonoBehaviour
{
    [SerializeField] private TrainingTrackManager trackManager;
    [SerializeField] private string stateName;
    private bool _wasPlayed = false;

    private void Awake()
    {
        trackManager.onRestart += ResetTrigger;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_wasPlayed)
        {
            trackManager.onStateChange?.Invoke(stateName);
            _wasPlayed = true;
        }
    }
    public void ResetTrigger()
    {
        _wasPlayed = false;
    }
}
