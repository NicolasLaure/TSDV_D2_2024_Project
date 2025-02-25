using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class FSM
    {
        [SerializeField] private List<State> states = new List<State>();

        private State _currentState;
        private bool _isDisabled;

        public FSM(List<State> states)
        {
            this.states = states;
            _isDisabled = false;
            _currentState = states[0];
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }
        public void ChangeState<T>(T newState) where T : State
        {
            if (_isDisabled)
                return;

            if (_currentState == newState)
            {
                _currentState.Exit();
                _currentState.Enter();
            }
            else if (states.Contains(newState))
            {
                _currentState = newState;
                _currentState.Enter();
            }
        }

        public void Enable()
        {
            _isDisabled = false;
        }

        public void Disable()
        {
            _isDisabled = true;
        }

        public State GetCurrentState()
        {
            return _currentState;
        }
    }
}