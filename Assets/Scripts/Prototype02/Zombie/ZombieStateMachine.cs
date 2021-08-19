namespace Prototype02.Zombie
{
    public class ZombieStateMachine
    {
        public ZombieState CurrentState { get; private set; }

        public void Initialize(ZombieState startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(ZombieState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}