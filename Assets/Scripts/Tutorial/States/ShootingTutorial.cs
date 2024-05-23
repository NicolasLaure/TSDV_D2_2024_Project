using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTutorial : TutorialState
{
    [SerializeField] private List<Transform> targetPositions = new List<Transform>();
    [SerializeField] private GameObject target;

    [Header("Panel management")]
    [SerializeField] private float panelDuration;
    [SerializeField] private float endPanelDuration;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject truckPanel;
    protected override IEnumerator StartStateCoroutine()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(panelDuration);
        warningPanel.SetActive(false);

        yield return TargetsPop();

        truckPanel.SetActive(true);
        yield return new WaitForSeconds(endPanelDuration);
        truckPanel.SetActive(false);

        onStateFinished.Invoke();
    }

    private IEnumerator TargetsPop()
    {
        for (int i = 0; i < targetPositions.Count; i++)
        {
            target.transform.position = targetPositions[i].position;
            target.transform.rotation = targetPositions[i].rotation;
            target.SetActive(true);
            yield return new WaitUntil(WasTargetHitted);
        }
    }
    private bool WasTargetHitted()
    {
        return !target.activeInHierarchy;
    }
}
