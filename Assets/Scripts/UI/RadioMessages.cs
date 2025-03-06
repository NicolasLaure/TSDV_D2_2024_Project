using System;
using System.Collections;
using UnityEngine;

public class RadioMessages : MonoBehaviour
{
    [SerializeField] private float textDuration;
    [SerializeField] private GameObject rangeText;
    [SerializeField] private GameObject helicopterText;

    private void Start()
    {
        StartCoroutine(ShowTexts());
    }

    private IEnumerator ShowText(GameObject text)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(textDuration);
        text.SetActive(false);
    }

    private IEnumerator ShowTexts()
    {
        yield return ShowText(rangeText);
        yield return ShowText(helicopterText);
    }
}