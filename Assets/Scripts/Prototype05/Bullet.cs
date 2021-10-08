using System;
using UnityEditor;
using UnityEngine;

namespace Prototype05
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameProperties _properties;
        
        private Vector3 Direction { get; set; }
        private float _shootTime;
        private BulletPool _pool;
        private Vector3 _velocity;
        private Vector3 _explosiveness;

        public void ShootBy(Transform shooter, BulletPool pool)
        {
            _pool = pool;
            _shootTime = Time.time;
            transform.SetPositionAndRotation(shooter.position, shooter.rotation);
            Direction = shooter.up;
            _velocity = Direction * _properties.bulletSpeed * Time.fixedDeltaTime;
            _explosiveness = _velocity;
        }
        
        private void FixedUpdate()
        {
            if (_shootTime + _properties.bulletLifeTime < Time.time)
            {
                _pool.Despawn(this);
                _pool = null;
            }
            else
            {
                _velocity = Direction * _properties.bulletSpeed * Time.fixedDeltaTime;
                _velocity += -_properties.bulletDrag * _velocity;
                transform.position += _velocity;
            }
        }

        private void OnDrawGizmos()
        {
            // Handles.Label(transform.position, _explosiveness.ToString());
        }
    }
}