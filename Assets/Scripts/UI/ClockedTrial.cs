using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockedTrial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }
    public void OnTimeUpdated(int timeLeft)
    {
        int minutes = timeLeft / 60;
        int seconds = timeLeft % 60;
        timeText.text = minutes + ":" + seconds;
    }
}
