using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionSensor : MonoBehaviour
    {
        private int _colliderCount = 0;

        public bool Colliding => _colliderCount > 0;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            _colliderCount++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _colliderCount--;
        }
    }
}