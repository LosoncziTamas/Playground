using System;
using Prototype02.Zombie;
using UnityEngine;

namespace Prototype02
{
    public class MovingWall : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        private HeroController _heroController;

        private void Start()
        {
            _heroController = HeroController.Instance;
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.right * _gameData.wallMovementSpeed * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Tags.EnemyTag))
            {
                var enemy = other.gameObject.GetComponent<ZombieController>();
                Debug.Assert(enemy != null);
                enemy.ZombieStateMachine.ChangeState(enemy.ZombieDeathState);
            }
            else if (other.gameObject.CompareTag(Tags.PlayerTag))
            {
                _heroController.HeroStateMachine.ChangeState(_heroController.HeroDeathState);
            }
        }
    }
}
