using UnityEngine;

namespace Prototype02
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        
        private HeroController _hero;
        
        private void Start()
        {
            _hero = HeroController.Instance;
        }
        
        private void Update()
        {
            var heroPos = _hero.transform.position;
            var camPos = transform.position;
            
            var horizontalOffset = heroPos.x - camPos.x;
            var horizontalDistance = Mathf.Abs(horizontalOffset);
            if (horizontalDistance > _gameData.cameraMovementMaxDistance)
            {
                var offset = horizontalDistance - _gameData.cameraMovementMaxDistance;
                transform.position += Vector3.right * offset * Mathf.Sign(horizontalOffset);
            }
            
            var verticalOffset = heroPos.y - camPos.y;
            var verticalDistance = Mathf.Abs(verticalOffset);
            if (verticalDistance > _gameData.cameraMovementMaxDistance)
            {
                var offset = verticalDistance - _gameData.cameraMovementMaxDistance;
                transform.position += Vector3.up * offset * Mathf.Sign(verticalOffset);
            }

        }
    }
}
