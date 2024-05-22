using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject trainingTargets;
    [SerializeField] private GameObject trial;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        trial.GetComponent<TimeTrial>().onTrialFinish += EndTrial;
    }

    private void EndTrial()
    {
        ChangeActiveMode(false);
    }
    public void ChangeActiveMode(bool shouldStartTrial)
    {
        trainingTargets.SetActive(!shouldStartTrial);
        trial.SetActive(shouldStartTrial);
    }
}