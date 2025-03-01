using System.Collections.Generic;
using FSM;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.BombEnemy
{
    public class BombEnemyAgent : Agent
    {
        [SerializeField] private HealthPoints healthPoints;

        [SerializeField] private ActionEventsWrapper roamingEvents;
        [SerializeField] private ActionEventsWrapper chaseEvents;
        [SerializeField] private ActionEventsWrapper detonateEvents;

        private State _roamingState;
        private State _chaseState;
        private State _detonateState;

        public void ChangeStateToRoaming()
        {
            if (Fsm.GetCurrentState().TryGetTransition(_roamingState, out Transition transition))
            {
                transition.Do();
                Fsm.ChangeState(_roamingState);
            }
        }

        public void ChangeStateToChase()
        {
            if (Fsm.GetCurrentState().TryGetTransition(_chaseState, out Transition transition))
            {
                transition.Do();
                Fsm.ChangeState(_chaseState);
            }
        }

        public void ChangeStateToDetonate()
        {
            if (Fsm.GetCurrentState().TryGetTransition(_detonateState, out Transition transition))
            {
                transition.Do();
                Fsm.ChangeState(_detonateState);
            }
        }

        protected override List<State> GetStates()
        {
            _roamingState = new State();
            _roamingState.EnterAction += roamingEvents.ExecuteOnEnter;
            _roamingState.UpdateAction += roamingEvents.ExecuteOnUpdate;
            _roamingState.ExitAction += roamingEvents.ExecuteOnExit;

            _chaseState = new State();
            _chaseState.EnterAction += chaseEvents.ExecuteOnEnter;
            _chaseState.UpdateAction += chaseEvents.ExecuteOnUpdate;
            _chaseState.ExitAction += chaseEvents.ExecuteOnExit;

            _detonateState = new State();
            _detonateState.EnterAction += detonateEvents.ExecuteOnEnter;
            _detonateState.UpdateAction += detonateEvents.ExecuteOnUpdate;
            _detonateState.ExitAction += detonateEvents.ExecuteOnExit;

            Transition roamingToChaseTransition = new Transition(_roamingState, _chaseState);
            _roamingState.AddTransition(roamingToChaseTransition);

            Transition chaseToRoamingTransition = new Transition(_chaseState, _roamingState);
            _chaseState.AddTransition(chaseToRoamingTransition);

            Transition chaseToDetonateTransition = new Transition(_chaseState, _detonateState);
            _chaseState.AddTransition(chaseToDetonateTransition);

            return new List<State>()
            {
                _roamingState,
                _chaseState,
                _detonateState
            };
        }
    }
}