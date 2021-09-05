using System;
using UnityEngine;

namespace Prototype02
{
    public class MovingWall : MonoBehaviour
    {
        // TODO: set tags properly
        // TODO: update colliders
        [SerializeField] private GameData _gameData;

        private void FixedUpdate()
        {
            transform.position += Vector3.right * _gameData.wallMovementSpeed * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Tags.EnemyTag))
            {
                // TODO: set enemy state properly
            }
        }
    }
}
