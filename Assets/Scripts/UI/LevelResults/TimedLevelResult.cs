using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimedLevelResult : LevelResults
{
    [SerializeField] private float tier1Time;
    [SerializeField] private float tier2Time;
    [SerializeField] private float tier3Time;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    [HideInInspector] public float levelTime;
    [HideInInspector] public float bestTime;
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

        SetTexts();
        base.OnEnable();
    }

    private void SetTexts()
    {
        int minutes = Mathf.FloorToInt(levelTime) / 60;
        int seconds = Mathf.FloorToInt(levelTime) % 60;
        finalTimeText.text = "Final Time: " + minutes + ":" + seconds;

        minutes = Mathf.FloorToInt(bestTime) / 60;
        seconds = Mathf.FloorToInt(bestTime) % 60;
        bestTimeText.text = "Best Time: " + minutes + ":" + seconds;
    }
}
