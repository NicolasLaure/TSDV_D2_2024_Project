using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Enemy
{
    public class EnemyAgent : Agent
    {
        [SerializeField] private HealthPoints healthPoints;

        [SerializeField] private ActionEventsWrapper idleEvents;
        [SerializeField] private ActionEventsWrapper combatEvents;

        private State _idleState;
        private State _combatState;

        public void ChangeStateToIdle()
        {
            Fsm.ChangeState(_idleState);
        }

        public void ChangeStateToCombat()
        {
            Fsm.ChangeState(_combatState);
        }

        protected override List<State> GetStates()
        {
            _idleState = new State();
            _idleState.EnterAction += idleEvents.ExecuteOnEnter;
            _idleState.UpdateAction += idleEvents.ExecuteOnUpdate;
            _idleState.ExitAction += idleEvents.ExecuteOnExit;

            _combatState = new State();
            _combatState.EnterAction += combatEvents.ExecuteOnEnter;
            _combatState.UpdateAction += combatEvents.ExecuteOnUpdate;
            _combatState.ExitAction += combatEvents.ExecuteOnExit;

            Transition idleToCombatTransition = new Transition(_idleState, _combatState);
            _idleState.AddTransition(idleToCombatTransition);

            Transition combatToIdleTransition = new Transition(_combatState, _idleState);
            _combatState.AddTransition(combatToIdleTransition);

            return new List<State>()
            {
                _idleState,
                _combatState
            };
        }
    }
}