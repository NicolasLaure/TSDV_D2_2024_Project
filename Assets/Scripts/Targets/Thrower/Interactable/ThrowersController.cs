using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowersController : MonoBehaviour
{
    [SerializeField] private List<TargetThrower> throwers = new List<TargetThrower>();
    [SerializeField] private ThrowersStartButton button;
    private int targetsToThrowQty = 10;

    private void Start()
    {
        button.onStartThrowing += OnStartShooting;
    }
    private void OnStartShooting()
    {
        foreach (TargetThrower thrower in throwers)
        {
            if (thrower.throwCoroutine != null)
                StopCoroutine(thrower.throwCoroutine);

            thrower.TargetsQty = targetsToThrowQty / throwers.Count;
            thrower.throwCoroutine = StartCoroutine(thrower.StartThrowing());
        }
    }
}
