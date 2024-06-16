using System;
using System.Collections.Generic;
using UnityEngine;
public class TrainingTrackManager : MonoBehaviour
{
    [SerializeField] private List<TrackState> targetGroups = new List<TrackState>();
    public Action<string> onStateChange;
    public event Action onRestart;

    private void Awake()
    {
        onStateChange += AddState;
    }
    private void Start()
    {
        InitializeStates();
    }
    private void OnDestroy()
    {
        onStateChange -= AddState;
    }
    public void StartTrack()
    {
        InitializeStates();
        targetGroups[0].Enter();
        onRestart?.Invoke();
    }
    private void AddState(string stateName)
    {
        foreach (TrackState state in targetGroups)
        {
            if (state.StateName == stateName)
            {
                state.Enter();
            }
        }
    }
    private void InitializeStates()
    {
        foreach (TrackState state in targetGroups)
        {
            state.Initialize();
        }
    }
    public void StartTimer()
    {

    }
}