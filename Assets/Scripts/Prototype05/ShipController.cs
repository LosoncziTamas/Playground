using System;
using UnityEngine;

namespace Prototype05
{
    public class ShipController : MonoBehaviour
    {
        private struct ShipInput
        {
            public float rotation;
            public float movement;
        }
        
        [SerializeField] private ShipProperties _shipProperties;
        
        private Vector3 _velocity;
        private Transform _cachedTransform;
        private ShipInput _shipInput;
        
        private void Start()
        {
            _cachedTransform = transform;
        }

        private void Update()
        {
            _shipInput.rotation = -Input.GetAxis("Horizontal");
            _shipInput.movement = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            var rotation = _shipInput.rotation;
            var movement = _shipInput.movement;
            
            _cachedTransform.Rotate(Vector3.forward, rotation * _shipProperties.rotationSpeed * Time.fixedDeltaTime);
            
            if (Mathf.Abs(movement) > 0 && Mathf.Approximately(_velocity.magnitude, 0))
            {
                _velocity = _cachedTransform.right * movement * _shipProperties.speed * Time.fixedDeltaTime;
            }
            else
            {
                var acceleration = _cachedTransform.right * (movement * _shipProperties.speed * Time.fixedDeltaTime);
                acceleration += -_shipProperties.drag * _velocity;
                _velocity += acceleration;
                _velocity = Vector3.ClampMagnitude(_velocity, _shipProperties.maxVelocityMagnitude);
            }
            _cachedTransform.position += _velocity;
        }
    }
}
