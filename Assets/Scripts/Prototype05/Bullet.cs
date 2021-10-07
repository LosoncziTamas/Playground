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

        public void ShootBy(Transform shooter, BulletPool pool)
        {
            _pool = pool;
            _shootTime = Time.time;
            transform.SetPositionAndRotation(shooter.position, shooter.rotation);
            Direction = shooter.up;
            _velocity = Direction * _properties.bulletSpeed * Time.fixedDeltaTime;
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
                // TODO: correct this
                var acceleration = transform.up * (_properties.tankSpeed * Time.fixedDeltaTime);
                acceleration += -_properties.tankDrag * _velocity;
                _velocity += acceleration;
                _velocity = Vector3.ClampMagnitude(_velocity, _properties.tankMaxVelocityMagnitude);
            }

            transform.position += _velocity;
        }
    }
}