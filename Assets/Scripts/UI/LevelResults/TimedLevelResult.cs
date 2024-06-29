using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimedLevelResult : LevelResults
{
    [SerializeField] private List<float> tierTimes = new List<float>();
    [SerializeField] private TierRequirementTextHandler tierTexts;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    [HideInInspector] public float levelTime;
    [HideInInspector] public float bestTime;

    private void OnValidate()
    {
        tierTexts.UpdateRequirementTimers(tierTimes);
    }
    protected override void OnEnable()
    {
        if (tierTimes.Count >= 3 && levelTime < tierTimes[2])
            tiersAchieved = 3;
        else if (tierTimes.Count >= 2 && levelTime < tierTimes[1])
            tiersAchieved = 2;
        else if (tierTimes.Count >= 1 && levelTime < tierTimes[0])
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
