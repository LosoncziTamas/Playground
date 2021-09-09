using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Prototype03
{
    public class Node : MonoBehaviour
    {
        public enum NodeType
        {
            Empty,
            PlayerNode,
            EnemyNode
        }

        public NodeType nodeType;
        public Camera camera;
        public SpriteRenderer renderer;
        public Collider2D collider;
        public LineRenderer lineRenderer;
        
        private bool _selected;
        private bool _invadingOtherNode;
        private Node _otherNode;
        private NodeContainer _nodeContainer;

        public bool Selected { get; set; }

        public NodeContainer NodeContainer => _nodeContainer;
        public IdleNodeState IdleNodeState { get; set; }
        public SelectedNodeState SelectedNodeState { get; set; }
        public InvadingNodeState InvadingNodeState { get; set; }
        public NodeStateMachine NodeStateMachine { get; set; }
        
        private void Start()
        {
            if (nodeType == NodeType.Empty)
            {
                renderer.color = Color.white;
            }
            else if (nodeType == NodeType.PlayerNode)
            {
                renderer.color = Color.blue;
            }
            else if (nodeType == NodeType.PlayerNode)
            {
                renderer.color = Color.red;
            }
            _nodeContainer = NodeContainer.Instance;

            NodeStateMachine = new NodeStateMachine();
            SelectedNodeState = new SelectedNodeState(this, NodeStateMachine);
            IdleNodeState = new IdleNodeState(this, NodeStateMachine);
            InvadingNodeState = new InvadingNodeState(this, NodeStateMachine);

            NodeStateMachine.Initialize(IdleNodeState);
        }
        
        private void Update()
        {
            NodeStateMachine.CurrentState.Update();

            return;
            var buttonPressed = Input.GetMouseButton(0);
            var mousePos = Input.mousePosition;

            if (nodeType != NodeType.PlayerNode)
            {
                return;
            }
            
            if (_invadingOtherNode)
            {
                DrawSegment(transform.position, _otherNode.transform.position);
                if (buttonPressed)
                {
                    var worldPos = camera.ScreenToWorldPoint(mousePos);
                    if (collider.OverlapPoint(worldPos))
                    {
                        _invadingOtherNode = false;
                    }
                }
            }
            

            for (var i = 0; i < _nodeContainer.nodes.Count; i++)
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

        private void OnDrawGizmos()
        {
            var text = _invadingOtherNode ? "invading node" : string.Empty;
            text += _selected ? " selected" : string.Empty;
            Handles.Label(transform.position, text);
        }

        public void DrawSegment(Vector3 from, Vector3 to)
        {
            lineRenderer.SetPosition(0, from);
            lineRenderer.SetPosition(1, to);
        }
        
        
    }
}
