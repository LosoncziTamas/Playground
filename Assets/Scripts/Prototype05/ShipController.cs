using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Prototype05
{
    public class ShipController : MonoBehaviour
    {
        private struct ShipInput
        {
            public float rotation;
            public float movement;
        }

        private const string ObstacleTag = "Land";
        
        [FormerlySerializedAs("_shipProperties")] [SerializeField] private GameProperties _properties;
        
        private Vector3 _velocity;
        private Transform _cachedTransform;
        private ShipInput _shipInput;
        private List<ContactPoint2D> _contactPoints = new List<ContactPoint2D>();
        
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
            
            _cachedTransform.Rotate(Vector3.forward, rotation * _properties.tankRotationSpeed * Time.fixedDeltaTime);
            
            if (Mathf.Abs(movement) > 0 && Mathf.Approximately(_velocity.magnitude, 0))
            {
                _velocity = _cachedTransform.up * (movement * _properties.tankSpeed * Time.fixedDeltaTime);
            }
            else
            {
                var acceleration = _cachedTransform.up * (movement * _properties.tankSpeed * Time.fixedDeltaTime);
                acceleration += -_properties.tankDrag * _velocity;
                _velocity += acceleration;
                _velocity = Vector3.ClampMagnitude(_velocity, _properties.tankMaxVelocityMagnitude);
            }
            _cachedTransform.position += _velocity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (var contactPoint in _contactPoints)
            {
                Gizmos.DrawCube(contactPoint.point, Vector3.one * 0.1f);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(ObstacleTag))
            {
                other.GetContacts(_contactPoints);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(ObstacleTag))
            {
                other.GetContacts(_contactPoints);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(ObstacleTag))
            {
                _contactPoints.Clear();
            }
        }
    }
}
