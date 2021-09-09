using UnityEngine;

namespace Prototype03
{
    public class IdleNodeState : NodeState
    {
        public IdleNodeState(Node node, NodeStateMachine nodeStateMachine) : base(node, nodeStateMachine)
        {
            
        }

        public override void Enter()
        {
            Node.DrawSegment(Vector3.zero, Vector3.zero);
        }

        public override void Update()
        {
            var buttonPressed = Input.GetMouseButton(0);
            var mousePos = Input.mousePosition;
            
            if (buttonPressed)
            {
                var worldPos = Node.camera.ScreenToWorldPoint(mousePos);
                if (Node.collider.OverlapPoint(worldPos))
                {
                    if (Node.nodeType == Node.NodeType.PlayerNode)
                    {
                        NodeStateMachine.ChangeState(Node.SelectedNodeState);
                    }
                }
            }
        }
    }
}