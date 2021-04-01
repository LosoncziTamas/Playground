using UnityEngine;

namespace Prototype01
{
    public class Pointer : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }
}