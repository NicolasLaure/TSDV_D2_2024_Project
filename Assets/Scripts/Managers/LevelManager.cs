using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject trainingTargets;
    [SerializeField] private GameObject trial;

    public void ChangeActiveMode(bool shouldStartTrial)
    {
        trainingTargets.SetActive(!shouldStartTrial);
        trial.SetActive(shouldStartTrial);
    }
}
