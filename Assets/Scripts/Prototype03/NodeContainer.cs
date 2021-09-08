using System.Collections.Generic;
using UnityEngine;

namespace Prototype03
{
    public class NodeContainer : MonoBehaviour
    {
        public static NodeContainer Instance { get; set; }
        
        public readonly List<Node> nodes = new List<Node>();

        private void Awake()
        {
            Instance = this;
            foreach (Transform trans in transform)
            {
                var node = trans.gameObject.GetComponent<Node>();
                if (node != null)
                {
                    nodes.Add(node);
                }
            }
        }
    }
}