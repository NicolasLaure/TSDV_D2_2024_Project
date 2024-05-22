using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TrialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    public void OnTimeUpdated(int timeLeft)
    {
        int minutes = timeLeft / 60;
        int seconds = timeLeft % 60;
        timeText.text = minutes + ":" + seconds;
    }

    public void OnScoreUpdated(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
