using System.Collections.Generic;
using UnityEngine;

public class TimedLevelResult : LevelResults
{
    [SerializeField] private List<float> tierTimes = new List<float>();

    private void OnValidate()
    {
        tierTexts.UpdateRequirements(tierTimes);
    }
    protected override void OnEnable()
    {
        if (tierTimes.Count >= 3 && levelScore < tierTimes[2])
            tiersAchieved = 3;
        else if (tierTimes.Count >= 2 && levelScore < tierTimes[1])
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
        int seconds = Mathf.FloorToInt(levelScore) % 60;
        resultText.text = "Final Time:  " + seconds + "s";

        seconds = Mathf.FloorToInt(bestScore) % 60;
        bestResultText.text = "Best Time:  " + seconds + "s";
    }
}
