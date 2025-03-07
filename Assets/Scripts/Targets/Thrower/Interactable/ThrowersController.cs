using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowersController : MonoBehaviour
{
    [SerializeField] private List<TargetThrower> throwers = new List<TargetThrower>();
    [SerializeField] private ThrowersStartButton button;
    [SerializeField] private ThrowerModifier decrease;
    [SerializeField] private ThrowerModifier increase;
    [SerializeField] private TextMeshPro qtyText;
    private int _targetsToThrowQty = 10;

    private void Start()
    {
        button.onStartThrowing += OnStartShooting;
        decrease.onModifierHit += OnModifyQty;
        increase.onModifierHit += OnModifyQty;
    }
    private void OnStartShooting()
    {
        foreach (TargetThrower thrower in throwers)
        {
            if (thrower.throwCoroutine != null)
                StopCoroutine(thrower.throwCoroutine);

            thrower.TargetsQty = _targetsToThrowQty / throwers.Count;
            thrower.throwCoroutine = StartCoroutine(thrower.StartThrowing());
        }
    }

    private void OnModifyQty(int value)
    {
        _targetsToThrowQty += value;
        qtyText.text = _targetsToThrowQty.ToString();
    }

}
