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
                // TODO: fix wall collision
                var enemy = other.gameObject.GetComponent<ZombieController>();
                Debug.Assert(enemy != null);
                if (enemy.Activated)
                {
                    enemy.ZombieStateMachine.ChangeState(enemy.ZombieDeathState);
                }
                else if (enemy.ZombieStateMachine.CurrentState != enemy.ZombieDeathState)
                {
                    enemy.gameObject.SetActive(false);
                }
            }
            else if (other.gameObject.CompareTag(Tags.PlayerTag))
            {
                _heroController.HeroStateMachine.ChangeState(_heroController.HeroDeathState);
            }
        }
    }
}
