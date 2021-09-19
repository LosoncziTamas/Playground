using UnityEngine;

namespace Prototype02
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private GameObject _wall;
        
        private HeroController _hero;
        
        private void Start()
        {
            _hero = HeroController.Instance;
        }
        
        private void Update()
        {
            var heroPos = _hero.transform.position;
            var wallPos= _wall.transform.position;
            var camPos = transform.position;
            
            var horizontalOffset = wallPos.x - camPos.x;
            var horizontalDistance = Mathf.Abs(horizontalOffset);

            var offset = horizontalDistance - _gameData.cameraMovementMaxDistance;
            transform.position += Vector3.right * (offset * Mathf.Sign(horizontalOffset) + _gameData.cameraWallOffset);
            
            var verticalOffset = heroPos.y - camPos.y;
            var verticalDistance = Mathf.Abs(verticalOffset);
            if (verticalDistance > _gameData.cameraVerticalMovementMaxDistance)
            {
                var delta = verticalDistance - _gameData.cameraMovementMaxDistance;
                transform.position += Vector3.up * delta * Mathf.Sign(verticalOffset);
            }
        }
    }
}
