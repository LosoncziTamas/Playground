using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionSensor : MonoBehaviour
    {
        public const string EnemyTag = "Enemy";
        
        private int _colliderCount = 0;
        private bool _collidingWithEnemy;

        public bool Colliding => _colliderCount > 0;

        public bool CollidingWithEnemy => _collidingWithEnemy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _colliderCount++;
            _collidingWithEnemy = other.CompareTag(EnemyTag);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _colliderCount--;
            _collidingWithEnemy = false;
        }
    }
}