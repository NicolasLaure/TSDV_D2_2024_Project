using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedTutorialState : TutorialState
{
    [SerializeField] TruckBehaviour truck;


    protected override IEnumerator StartStateCoroutine()
    {
        truck.EnableCollider();
        yield break;
    }
}
