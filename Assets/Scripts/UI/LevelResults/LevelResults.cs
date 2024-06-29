using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResults : MonoBehaviour
{
    [SerializeField] private List<GameObject> tierObjects = new List<GameObject>();

    [HideInInspector] public int tiersAchieved = 0;
    protected virtual void OnEnable()
    {
        for (int i = 0; i < tiersAchieved; i++)
        {
            tierObjects[i].SetActive(true);
        }
    }
}
