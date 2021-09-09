using UnityEngine;
using static Prototype03.Node;

namespace Prototype03
{
    public class SelectedNodeState : NodeState
    {
        public SelectedNodeState(Node node, NodeStateMachine nodeStateMachine) : base(node, nodeStateMachine)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Update()
        {
            var buttonPressed = Input.GetMouseButton(0);
            if (!buttonPressed)
            {
                NodeStateMachine.ChangeState(Node.IdleNodeState);
                return;
            }

            var mousePos = Input.mousePosition;
            var worldPos = Node.camera.ScreenToWorldPoint(mousePos);
            if (Node.nodeType == NodeType.PlayerNode)
            {
                Node.DrawSegment(worldPos, Node.transform.position);
                var nodes = Node.NodeContainer.nodes;
                for (var i = 0; i < Node.NodeContainer.nodes.Count; i++)
                {

                    var node = _nodeContainer.nodes[i];
                    if (node.Selected && node.nodeType != NodeType.PlayerNode)
                    {
                        _otherNode = node;
                        _invadingOtherNode = true;
                        DrawSegment(transform.position, node.transform.position);
                        return;
                    }
                }
            }
        }
    }
}