namespace Prototype03
{
    public class NodeStateMachine
    {
        public NodeState CurrentState { get; private set; }

        public void Initialize(NodeState state)
        {
            CurrentState = state;
            CurrentState.Enter();
        }

        public void ChangeState(NodeState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}