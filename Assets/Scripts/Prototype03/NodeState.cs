namespace Prototype03
{
    public class NodeState
    {
        protected Node Node;
        protected NodeStateMachine NodeStateMachine;

        public NodeState(Node node, NodeStateMachine nodeStateMachine)
        {
            Node = node;
            NodeStateMachine = nodeStateMachine;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
            
        }
        
    }
}