using System;
using UnityEngine;

namespace Prototype03
{
    public class Node : MonoBehaviour
    {
        public enum NodeState
        {
            Empty,
            PlayerNode,
            EnemyNode
        }

        public NodeState nodeState;
        public Camera camera;
        public SpriteRenderer renderer;
        public Collider2D collider;
        public LineRenderer lineRenderer;
        private bool _selected;
        private bool _invadingOtherNode;
        private Node _otherNode;
        private NodeContainer _nodeContainer;

        public bool Selected => _selected;
        
        private void Start()
        {
            if (nodeState == NodeState.Empty)
            {
                renderer.color = Color.white;
            }
            else if (nodeState == NodeState.PlayerNode)
            {
                renderer.color = Color.blue;
            }
            else if (nodeState == NodeState.PlayerNode)
            {
                renderer.color = Color.red;
            }
            _nodeContainer = NodeContainer.Instance;
        }

        private void Update()
        {
            var buttonPressed = Input.GetMouseButton(0);
            var mousePos = Input.mousePosition;

            if (_invadingOtherNode)
            {
                DrawSegment(transform.position, _otherNode.transform.position);
                // TODO: use state machine
            }
            
            if (buttonPressed && !_selected)
            {
                var worldPos = camera.ScreenToWorldPoint(mousePos);
                if (collider.OverlapPoint(worldPos))
                {
                    _selected = true;
                    if (nodeState == NodeState.PlayerNode)
                    {
                        DrawSegment(worldPos, transform.position);
                    }
                }
            }
            else if (buttonPressed && _selected)
            {
                var worldPos = camera.ScreenToWorldPoint(mousePos);
                if (nodeState == NodeState.PlayerNode)
                {
                    DrawSegment(worldPos, transform.position);
                }
            }
            else if (!buttonPressed)
            {
                _selected = false;
                if (nodeState == NodeState.PlayerNode)
                {
                    lineRenderer.SetPosition(0, Vector3.zero);
                    lineRenderer.SetPosition(1, Vector3.zero);                
                }
            }

            for (var i = 0; i < _nodeContainer.nodes.Count; i++)
            {
                var node = _nodeContainer.nodes[i];
                if (node.Selected && node.nodeState != NodeState.PlayerNode)
                {
                    _otherNode = node;
                    _invadingOtherNode = true;
                    DrawSegment(transform.position, node.transform.position);
                    return;
                }
            }
        }

        private void DrawSegment(Vector3 from, Vector3 to)
        {
            lineRenderer.SetPosition(0, from);
            lineRenderer.SetPosition(1, to);
        }
        
        
    }
}
