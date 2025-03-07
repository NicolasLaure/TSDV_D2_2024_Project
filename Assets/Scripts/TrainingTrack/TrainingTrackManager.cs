using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTrackManager : MonoBehaviour
{
    [SerializeField] private List<TrackState> targetGroups = new List<TrackState>();
    [SerializeField] private TimedLevelResult results;
    [SerializeField] private ClockedTrial timer;
    [SerializeField] private GameSaveSO save;

    public Action<string> onStateChange;
    public event Action onRestart;
    private Coroutine _trackCoroutine = null;
    private float _elapsedTime = 0;
    private float _startTime = 0;
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
        if (_trackCoroutine != null)
            StopCoroutine(_trackCoroutine);

        _trackCoroutine = StartCoroutine(TrackCoroutine());
    }
    private IEnumerator TrackCoroutine()
    {
        InitializeStates();
        targetGroups[0].Enter();
        StartTimer();
        timer.gameObject.SetActive(true);
        onRestart?.Invoke();
        while (!AreAllTargetsDown())
        {
            _elapsedTime = Time.time - _startTime;
            timer.OnTimeUpdated(Mathf.FloorToInt(_elapsedTime));
            yield return null;
        }
        Debug.Log(_elapsedTime);
        if (save.bestTime == 0 || _elapsedTime < save.bestTime)
            save.bestTime = _elapsedTime;

        timer.gameObject.SetActive(false);
        if (results != null)
        {
            results.SetScores(_elapsedTime, save.bestTime);
            results.gameObject.SetActive(true);
        }
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
    private bool AreAllTargetsDown()
    {
        foreach (TrackState state in targetGroups)
        {
            foreach (TrackTarget target in state.Targets)
            {
                if (!target.WasShotDown)
                    return false;
            }
        }
        return true;
    }
    public void StartTimer()
    {
        _elapsedTime = 0;
        _startTime = Time.time;
    }
}