using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TierRequirementTextHandler : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> requirementTexts = new List<TextMeshProUGUI>();

    public void UpdateRequirementTimers(List<float> tierTimes)
    {
        for (int i = 0; i < requirementTexts.Count; i++)
        {
            requirementTexts[i].text = Mathf.FloorToInt(tierTimes[i]) + "s";
        }
    }
}
