using System;
using UnityEngine;

namespace Prototype05
{
    public class ShipController : MonoBehaviour
    {
        private Vector3 _velocity;
        private Transform _cachedTransform;

        private void Start()
        {
            _cachedTransform = transform;
        }

        private void Update()
        {
            var rotation = -Input.GetAxis("Horizontal");
            var acceleration = Input.GetAxis("Vertical");
            var vec3Acceleration = _cachedTransform.right * acceleration;
            _cachedTransform.Rotate(Vector3.forward, rotation);
            var drag = -0.1f;
            var speed = 5.0f;
            _velocity.x += Math.Max(vec3Acceleration.x + drag, 0) * speed;
            _velocity.y += Math.Max(vec3Acceleration.y + drag, 0) * speed;
            _cachedTransform.position += _velocity;
        }
    }
}
