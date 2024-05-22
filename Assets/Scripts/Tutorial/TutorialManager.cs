using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialState> tutorialStates = new List<TutorialState>();
    private TutorialState currentState;

    private int currentIndex = 0;
    private void Start()
    {
        if (tutorialStates.Count > 0)
        {
            currentState = tutorialStates[currentIndex];
            currentState.onStateFinished += NextState;
            currentState.StartState();
        }
    }

    private void NextState()
    {
        if (currentState != null)
            currentState.onStateFinished -= NextState;

        currentIndex++;
        currentState = tutorialStates[currentIndex];
        currentState.onStateFinished += NextState;
        currentState.StartState();

        Debug.Log($"The Current state is: {currentState.name}");
    }

}
