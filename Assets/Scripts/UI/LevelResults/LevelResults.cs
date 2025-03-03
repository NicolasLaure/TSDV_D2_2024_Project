using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelResults : MonoBehaviour
{
    [SerializeField] private List<GameObject> tierObjects = new List<GameObject>();
    [SerializeField] private bool shouldAutomaticallyDisable;
    [SerializeField] private float panelDuration;
    [SerializeField] private float timeBetweenMedals;
    [SerializeField] protected TierRequirementTextHandler tierTexts;
    [SerializeField] protected TextMeshProUGUI resultText;
    [SerializeField] protected TextMeshProUGUI bestResultText;

    [HideInInspector] protected float levelScore;
    [HideInInspector] protected float bestScore;
    [HideInInspector] public int tiersAchieved = 0;

    protected virtual void OnEnable()
    {
        StartCoroutine(ShowMedals());
        if (shouldAutomaticallyDisable)
            StartCoroutine(TurnOffPanel());
    }

    private IEnumerator ShowMedals()
    {
        for (int i = 0; i < tiersAchieved; i++)
        {
            tierObjects[i].SetActive(true);
            yield return new WaitForSeconds(timeBetweenMedals);
        }
    }

    private IEnumerator TurnOffPanel()
    {
        float startTime = Time.time;
        float timer = 0;
        while (timer < panelDuration)
        {
            timer = Time.time - startTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }


    public void SetScores(float currentScore, float bestScore)
    {
        this.levelScore = currentScore;
        this.bestScore = bestScore;
    }
}