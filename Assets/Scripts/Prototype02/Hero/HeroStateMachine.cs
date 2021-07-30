using UnityEngine;

namespace Prototype02.New
{
    public class HeroStateMachine
    {
        public HeroState CurrentState { get; private set; }

        public void Initialize(HeroState startState)
        {
            //Debug.Log($"[HeroStateMachine] Initialize {startState.GetType()}");
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(HeroState newState)
        {
            //Debug.Log($"[HeroStateMachine] ChangeState {newState.GetType()}");
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}