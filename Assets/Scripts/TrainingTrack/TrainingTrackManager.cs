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
        foreach (TrackState state in targetGroups)
        {
            state.Initialize();
        }
    }
    public void StartTrack()
    {
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
    public void StartTimer()
    {

    }
}