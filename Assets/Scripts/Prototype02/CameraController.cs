using System;
using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        
        private Camera _camera;
        private HeroController _hero;
        
        private void Start()
        {
            _camera = GetComponent<Camera>();
            _hero = HeroController.Instance;
        }

        private void Update()
        {
            if (_hero.Moving)
            {
                var distance = Vector2.Distance(_hero.transform.position, transform.position);
                Debug.Log("Distance " + distance);
                if (distance > _gameData.cameraMovementMaxDistance)
                {
                    var currPos = transform.position;
                    var target = Vector2.MoveTowards(currPos, _hero.transform.position + _gameData.cameraHeroOffset, _gameData.cameraCatchUpSpeed * Time.deltaTime);
                    transform.position = new Vector3(target.x, target.y, currPos.z);
                } 
                else  if (distance > _gameData.cameraMovementMinDistance)
                {
                    var currPos = transform.position;
                    var target = Vector2.MoveTowards(currPos, _hero.transform.position + _gameData.cameraHeroOffset, _gameData.cameraSpeedDuringMovement * Time.deltaTime);
                    transform.position = new Vector3(target.x, target.y, currPos.z);
                }
            }
            else
            {
                var currPos = transform.position;
                var target = Vector2.MoveTowards(currPos, _hero.transform.position + _gameData.cameraHeroOffset, _gameData.cameraCatchUpSpeed * Time.deltaTime);
                transform.position = new Vector3(target.x, target.y, currPos.z);
            }
        }
    }
}
