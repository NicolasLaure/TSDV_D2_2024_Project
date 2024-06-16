using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrackState
{
    [SerializeField] private string stateName;
    [SerializeField] private List<TrackTarget> targets = new List<TrackTarget>();

    public List<TrackTarget> Targets { get { return targets; } }
    public string StateName { get { return stateName; } }
    public void Initialize()
    {
        foreach (TrackTarget target in targets)
        {
            target.SetDown();
        }
    }
    public void Enter()
    {
        foreach (TrackTarget target in targets)
        {
            target.GetUp();
        }
    }
}
