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

        private State _roamingState;
        private State _chaseState;

        public void ChangeStateToRoaming()
        {
            Fsm.ChangeState(_roamingState);
        }

        public void ChangeStateToChase()
        {
            Fsm.ChangeState(_chaseState);
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

            Transition roamingToChaseTransition = new Transition(_roamingState, _chaseState);
            _roamingState.AddTransition(roamingToChaseTransition);

            Transition chaseToRoamingTransition = new Transition(_chaseState, _roamingState);
            _chaseState.AddTransition(chaseToRoamingTransition);

            return new List<State>()
            {
                _roamingState,
                _chaseState
            };
        }
    }
}