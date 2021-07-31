using System.Collections.Generic;
using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyHitBox : MonoBehaviour
    {
        public string TagToCompare;

        public readonly List<Collider2D> EnemyColliders = new List<Collider2D>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagToCompare) && !EnemyColliders.Contains(other))
            {
                EnemyColliders.Add(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagToCompare) && EnemyColliders.Contains(other))
            {
                EnemyColliders.Remove(other);
            }
        }
    }
}