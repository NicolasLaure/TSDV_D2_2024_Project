using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTutorial : TutorialState
{
    [SerializeField] private List<Transform> targetPositions = new List<Transform>();
    [SerializeField] private GameObject target;

    [Header("Panel management")]
    [SerializeField] private float panelDuration;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject truckPanel;
    protected override IEnumerator StartStateCoroutine()
    {
        target.GetComponent<TrialTarget>().onTrialTargetShot += OnTargetHitted;
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(panelDuration);
        warningPanel.SetActive(false);

        yield return TargetsPop();

        truckPanel.SetActive(true);
        yield return new WaitForSeconds(panelDuration);
        truckPanel.SetActive(false);

        target.GetComponent<TrialTarget>().onTrialTargetShot -= OnTargetHitted;
        onStateFinished.Invoke();
    }

    private IEnumerator TargetsPop()
    {
        for (int i = 0; i < targetPositions.Count; i++)
        {
            target.transform.position = targetPositions[i].position;
            target.transform.forward = targetPositions[i].forward;
            target.SetActive(true);
            yield return new WaitUntil(WasTargetHitted);
        }
        yield break;
    }

    private void OnTargetHitted(bool isEnemy)
    {

    }
    private bool WasTargetHitted()
    {
        return !target.activeInHierarchy;
    }
}
