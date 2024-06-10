using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [SerializeField] private List<State> states = new List<State>();

    private State currentState;

    private void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    private void Update()
    {
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    private void LateUpdate()
    {
        currentState.LateUpdate();
    }

    public void ChangeState<T>(T newState) where T : State
    {
        if (currentState != null)
            currentState.Exit();

        if (states.Contains(newState))
        {
            currentState = newState;
            currentState.Enter(gameObject);
        }
    }
}
