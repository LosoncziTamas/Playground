using Prototype02.New;
using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieStateMachine
    {
        public ZombieState CurrentState { get; private set; }

        public void Initialize(ZombieState startState)
        {
            Debug.Log($"[ZombieStateMachine] Initialize {startState.GetType()}");
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(ZombieState newState)
        {
            Debug.Log($"[ZombieStateMachine] ChangeState {newState.GetType()}");
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}