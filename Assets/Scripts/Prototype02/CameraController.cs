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
            var horizontalOffset = _hero.transform.position.x - transform.position.x;
            var distance = Mathf.Abs(horizontalOffset);
            if (distance > _gameData.cameraMovementMaxDistance)
            {
                var offset = distance - _gameData.cameraMovementMaxDistance;
                transform.position += Vector3.right * offset * Mathf.Sign(horizontalOffset);
            }
        }
    }
}
