using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Prototype05
{
    public class LookTowardMouse : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _worldPos;
        private float _angle;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            _worldPos = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            _angle = Vector3.SignedAngle(_worldPos, Vector3.right, -Vector3.forward);
            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position, _angle.ToString(CultureInfo.InvariantCulture));
            Gizmos.DrawCube(_worldPos, Vector3.one * 0.2f);
            Gizmos.DrawLine(transform.position, _worldPos);
        }
    }
}