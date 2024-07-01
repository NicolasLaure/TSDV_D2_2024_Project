using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBasedLevelResult : LevelResults
{
    [SerializeField] private List<float> scoreTiers = new List<float>();

    private void OnValidate()
    {
        tierTexts.UpdateRequirements(scoreTiers);
    }

    protected override void OnEnable()
    {
        if (scoreTiers.Count >= 3 && levelScore >= scoreTiers[2])
            tiersAchieved = 3;
        else if (scoreTiers.Count >= 2 && levelScore >= scoreTiers[1])
            tiersAchieved = 2;
        else if (scoreTiers.Count >= 1 && levelScore >= scoreTiers[0])
            tiersAchieved = 1;
        else
            tiersAchieved = 0;

        SetTexts();
        base.OnEnable();
    }

    private void SetTexts()
    {
        resultText.text = "Final Score:  " + levelScore;
        bestResultText.text = "Best Score:  " + bestScore;
    }
}
