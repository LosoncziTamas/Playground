using UnityEngine;

namespace Prototype02
{
    public class MovingWall : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        private void FixedUpdate()
        {
            transform.position += Vector3.right * _gameData.wallMovementSpeed * Time.fixedDeltaTime;
        }
    }
}
