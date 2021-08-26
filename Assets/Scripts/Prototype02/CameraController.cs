using System;
using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        private const float MaxDiff = 3.0f;
        private const float Speed = 5.0f;
        
        private Camera _camera;
        private HeroController _hero;
        
        private void Start()
        {
            _camera = GetComponent<Camera>();
            _hero = HeroController.Instance;
        }

        private void Update()
        {
            // TODO: add offset
            // TODO: follow in a smart way
            var distance = Vector2.Distance(_hero.transform.position, transform.position);
            Debug.Log(distance);
            if (distance > MaxDiff)
            {
                var currPos = transform.position;
                var target = Vector2.MoveTowards(currPos, _hero.transform.position, Speed * Time.deltaTime);
                transform.position = new Vector3(target.x, target.y, currPos.z);
            }
        }
    }
}
