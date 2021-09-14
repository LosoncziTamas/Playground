using System.Collections.Generic;
using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionSensor : MonoBehaviour
    {
        private int _colliderCount = 0;
        private int _enemyColliderCount = 0;

        public bool Colliding => _colliderCount > 0;

        public bool CollidingWithEnemy => _enemyColliderCount > 0;
        public Collider2D LastEnemyCollider { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tags.MainCamera))
            {
                return;
            }
            
            _colliderCount++;
            if (other.CompareTag(Tags.EnemyTag))
            {
                _enemyColliderCount++;
                LastEnemyCollider = other;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tags.MainCamera))
            {
                return;
            }
            
            _colliderCount--;
            if (other.CompareTag(Tags.EnemyTag))
            {
                _enemyColliderCount--;
            }

            if (_enemyColliderCount == 0)
            {
                LastEnemyCollider = null;
            }
        }
    }
}