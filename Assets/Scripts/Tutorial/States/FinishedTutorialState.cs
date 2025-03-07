using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedTutorialState : TutorialState
{
    [SerializeField] private TruckBehaviour truck;


    protected override IEnumerator StartStateCoroutine()
    {
        truck.EnableCollider();
        yield break;
    }
}
