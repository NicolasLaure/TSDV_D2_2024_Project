using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialState> tutorialStates = new List<TutorialState>();
    [SerializeField] private GameSaveSO saveFile;
    private TutorialState _currentState;
    private int _currentIndex = 0;
    private void Start()
    {
        if (tutorialStates.Count > 0)
        {
            _currentState = tutorialStates[_currentIndex];
            _currentState.onStateFinished += NextState;
            _currentState.StartState();
        }
    }

    private void NextState()
    {
        if (_currentState != null)
            _currentState.onStateFinished -= NextState;

        _currentIndex++;
        _currentState = tutorialStates[_currentIndex];
        _currentState.onStateFinished += NextState;
        _currentState.StartState();

        Debug.Log($"The Current state is: {_currentState.name}");
    }

    public void EndTutorial()
    {
        saveFile.wasTutorialFinished = true;
        Loader.ChangeScene("ShootingRange");
    }
}
