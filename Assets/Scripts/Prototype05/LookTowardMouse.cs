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
        private Vector3 _from;
        private Vector3 _to;
        private float _angle;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            _worldPos = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            _from = transform.position + transform.up - transform.position;
            _to = _worldPos - transform.position;
            _angle = Vector3.SignedAngle(_from, _to, Vector3.forward);
            transform.Rotate(Vector3.forward, _angle);
        }

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position, _angle.ToString(CultureInfo.InvariantCulture));
            Gizmos.DrawCube(_worldPos, Vector3.one * 0.2f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _to);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _from);
        }
    }
}