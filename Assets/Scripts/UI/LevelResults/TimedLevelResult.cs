using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimedLevelResult : LevelResults
{
    [SerializeField] private List<float> tierTimes = new List<float>();

    private void OnValidate()
    {
        tierTexts.UpdateRequirementTimers(tierTimes);
    }
    protected override void OnEnable()
    {
        if (tierTimes.Count >= 3 && levelScore < tierTimes[2])
            tiersAchieved = 3;
        else if (tierTimes.Count >= 2 && levelScore< tierTimes[1])
            tiersAchieved = 2;
        else if (tierTimes.Count >= 1 && levelScore < tierTimes[0])
            tiersAchieved = 1;
        else
            tiersAchieved = 0;

        SetTexts();
        base.OnEnable();
    }

    private void SetTexts()
    {
        int minutes = Mathf.FloorToInt(levelScore) / 60;
        int seconds = Mathf.FloorToInt(levelScore) % 60;
        resultText.text = "Final Time: " + minutes + ":" + seconds;

        minutes = Mathf.FloorToInt(bestScore) / 60;
        seconds = Mathf.FloorToInt(bestScore) % 60;
        bestResultText.text = "Best Time: " + minutes + ":" + seconds;
    }
}
