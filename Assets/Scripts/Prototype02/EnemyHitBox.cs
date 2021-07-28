using System.Collections.Generic;
using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyHitBox : MonoBehaviour
    {
        public const string EnemyTag = "Enemy";

        public readonly List<Collider2D> EnemyColliders = new List<Collider2D>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(EnemyTag) && !EnemyColliders.Contains(other))
            {
                EnemyColliders.Add(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(EnemyTag) && EnemyColliders.Contains(other))
            {
                EnemyColliders.Remove(other);
            }
        }

        private void OnGUI()
        {
            GUILayout.Space(100);
            GUILayout.Label($"[EnemyHitBox] EnemyColliders count {EnemyColliders.Count}");
        }
    }
}