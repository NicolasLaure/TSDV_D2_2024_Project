using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialScores : MonoBehaviour
{
    [SerializeField] private TextMeshPro lastScoreText;
    [SerializeField] private TextMeshPro highScoreText;

    [SerializeField] private TimeTrial trial;
    private void Awake()
    {
        trial.onTrialFinish += OnTrialFinished;
    }

    private void OnTrialFinished()
    {
        lastScoreText.text = "Last Score: " + trial.Score;
        highScoreText.text = "HighScore: " + trial.HighScore;
    }
}
