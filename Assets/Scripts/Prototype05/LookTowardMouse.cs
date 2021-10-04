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
            var offset = worldPos - transform.position;
            var projected = Vector3.ProjectOnPlane(worldPos, Vector3.forward);
            transform.LookAt(projected, Vector3.forward);
        }
    }
}