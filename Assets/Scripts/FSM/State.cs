using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class State
    {
        private List<Transition> _transitions = new List<Transition>();
        public event Action EnterAction;
        public event Action UpdateAction;
        public event Action FixedUpdateAction;
        public event Action ExitAction;

        public virtual void Enter()
        {
            EnterAction?.Invoke();
        }

        public virtual void Update()
        {
            UpdateAction?.Invoke();
        }

        public virtual void FixedUpdate()
        {
            FixedUpdateAction?.Invoke();
        }

        public virtual void Exit()
        {
            ExitAction?.Invoke();
        }

        public void AddTransition(Transition transition)
        {
            _transitions.Add(transition);
        }

        public bool TryGetTransition(State to, out Transition transition)
        {
            foreach (Transition _transition in _transitions)
            {
                if (_transition.To == to)
                {
                    transition = _transition;
                    return true;
                }
            }

            transition = null;
            return false;
        }
    }
}