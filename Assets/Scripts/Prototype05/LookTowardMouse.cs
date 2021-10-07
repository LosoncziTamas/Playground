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
        private Transform _transform;
        private float _angle;

        private void Start()
        {
            _camera = Camera.main;
            _transform = transform;
        }

        private void FixedUpdate()
        {
            _worldPos = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            var currPos = _transform.position;
            _from = transform.up;
            _to = _worldPos - currPos;
            _angle = Vector3.SignedAngle(_from, _to, Vector3.forward);
            transform.Rotate(Vector3.forward, _angle);
        }
    }
}