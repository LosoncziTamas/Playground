using System;
using UnityEngine;

namespace Prototype05
{
    public class EnemyBrain : MonoBehaviour
    {
        public float minDistanceForShooting;
        public float speed;
        private GameWorld _gameWorld;

        private void Start()
        {
            _gameWorld = FindObjectOfType<GameWorld>();
        }

        private void FixedUpdate()
        {
            var currPos = transform.position;
            var target = _gameWorld.ShipController.transform.position;
            if (Vector3.Distance(target, currPos) > minDistanceForShooting)
            {
                transform.position = Vector3.MoveTowards(currPos, target, Time.fixedDeltaTime * speed);
            }
            else
            {
                // TODO: start shooting
            }
        }
    }
}
