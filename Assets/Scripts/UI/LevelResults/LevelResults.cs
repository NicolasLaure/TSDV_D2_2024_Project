using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResults : MonoBehaviour
{
    [SerializeField] private List<GameObject> tierObjects = new List<GameObject>();
    [SerializeField] private float panelDuration;
    [HideInInspector] public int tiersAchieved = 0;
    protected virtual void OnEnable()
    {
        for (int i = 0; i < tiersAchieved; i++)
        {
            tierObjects[i].SetActive(true);
        }

        StartCoroutine(TurnOffPanel());
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
}
