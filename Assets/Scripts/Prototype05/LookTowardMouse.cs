using System;
using UnityEngine;

namespace Prototype05
{
    public class LookTowardMouse : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            var worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.LookAt(worldPos, Vector3.forward);
        }
    }
}