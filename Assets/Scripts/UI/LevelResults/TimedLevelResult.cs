using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLevelResult : LevelResults
{
    [SerializeField] private float tier1Time;
    [SerializeField] private float tier2Time;
    [SerializeField] private float tier3Time;

    [HideInInspector] public float levelTime;
    protected override void OnEnable()
    {
        if (levelTime < tier3Time)
            tiersAchieved = 3;
        else if (levelTime < tier2Time)
            tiersAchieved = 2;
        else if (levelTime < tier1Time)
            tiersAchieved = 1;
        else
            tiersAchieved = 0;

        base.OnEnable();
    }
}
