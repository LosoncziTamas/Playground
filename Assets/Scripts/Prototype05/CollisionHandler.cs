using UnityEngine;

namespace Prototype05
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionHandler : MonoBehaviour
    {
        public string TagToCollideWith;
        public CollisionBehaviour CollisionBehaviour;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(TagToCollideWith))
            {
                CollisionBehaviour.OnCollisionEnter();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(TagToCollideWith))
            {
                CollisionBehaviour.OnCollisionExit();
            }
        }
    }
}
