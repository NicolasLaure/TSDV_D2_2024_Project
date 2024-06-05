using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBehaviour : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    public void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        tutorialManager.EndTutorial();
    }
}
